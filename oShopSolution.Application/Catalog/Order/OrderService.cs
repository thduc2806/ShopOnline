using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using oShopSolution.Utilities.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.System.Users;

namespace oShopSolution.Application.Catalog.Order
{
	public class OrderService : IOrderService
	{
		private readonly OShopDbContext _context;
		private readonly IMapper _mapper;
		public OrderService(OShopDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PageResult<OrderViewModel>> GetOrder(GetOrderModel request)
		{
			var order = from o in _context.Orders
						select new { o };
			
			int total = await order.CountAsync();

			if (!string.IsNullOrEmpty(request.Keyword))
			{
				order = order.Where(c => c.o.Email.Contains(request.Keyword));
			}

			switch (request.SortBy)
			{
				case SortingConstant.Date:
					order = request.SortDir == "asc" ? order.OrderBy(c => c.o.OrderDate) : order.OrderByDescending(c => c.o.OrderDate);
					break;
				case SortingConstant.Name:
                    order = request.SortDir == "asc" ? order.OrderBy(c => c.o.Email) : order.OrderByDescending(c => c.o.Email);
                    break;
                case SortingConstant.Total:
                    order = request.SortDir == "asc" ? order.OrderBy(c => c.o.Amount) : order.OrderByDescending(c => c.o.Amount);
                    break;
                case SortingConstant.StatusPayment:
                    order = request.SortDir == "asc" ? order.OrderBy(c => c.o.isPayment) : order.OrderByDescending(c => c.o.isPayment);
                    break;

				default:
                    order = order.OrderByDescending(c => c.o.OrderDate);
                    break;
            }

			var data = await order.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new OrderViewModel()
			{
				Id = x.o.Id,
				OrderDate = x.o.OrderDate,
				Email = x.o.Email,
				Amount = x.o.Amount,
				isPayment = x.o.isPayment,
				Name = x.o.FullName
			}).ToListAsync();

			var pageResult = new PageResult<OrderViewModel>
			{
				TotalItems = total,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				Items = data
			};

			return pageResult;
		}

		public async Task<PageResult<OrderViewModel>> GetById(GetOrderByIdModel request)
		{
			request.PageSize = 10;
            var order = from o in _context.Orders
						where o.UserId.ToString() == request.UserId
                        select new { o };

            int total = await order.CountAsync();
            var data = await order.OrderByDescending(o => o.o.OrderDate).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new OrderViewModel()
            {
                Id = x.o.Id,
                OrderDate = x.o.OrderDate,
                Email = x.o.Email,
                Amount = x.o.Amount,
                isPayment = x.o.isPayment,
                Name = x.o.FullName,
                isCancle = x.o.isCancle
            }).ToListAsync();

			var pageResult = new PageResult<OrderViewModel>
			{
				TotalItems = total,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				TotalPrice = data.Sum(p => p.Amount),
                Items = data
            };

            return pageResult;
        }

		public async Task<OrderViewModel> GetByOrderId(int orderId)
		{
			var order = await _context.Orders.Where(o => o.Id == orderId).SingleOrDefaultAsync();
			if (order != null)
			{
				var result = _mapper.Map<Data.Entities.Order, OrderViewModel>(order);
				return _mapper.Map<Data.Entities.Order, OrderViewModel>(order);
			}

			return new OrderViewModel();
		}


        public async Task<int> CreateOrder(UserProfileViewModel model)
		{
			var order = new Data.Entities.Order()
			{
				OrderDate = DateTime.Now,
				UserId = new Guid(model.UserId),
				Email = model.Email,
				Address = model.Street,
				PhoneNumber = Convert.ToInt32(model.PhoneNumber),
				City = model.City,
				District = model.State,
				Ward = model.Ward,
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();
        
            return order.Id;
		}

        public async Task<bool> UpdatePayment(OrderModel model)
		{
			var order = await _context.Orders.Where(o => o.Id == model.OrderId).FirstOrDefaultAsync();
			order.Amount = Convert.ToDecimal(model.Amount);
            order.CurrencyCode = model.CurrencyCode;
            order.PaymentId = model.PaymentId;
            order.isPayment = true;


			await _context.SaveChangesAsync();

            if (model.Items.Count > 0)
            {
                foreach (var item in model.Items)
                {
                    await CreateOrderDetail(item.Price, order.Id, item.ProductId, item.Quantity);
                }

                await DeleteCart(model.UserId);
				return true;
            }
            return false;
        }

		public async Task<List<OrderDetailViewModel>> GetOrderDetail(int orderId)
		{
			var orderDetails = await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
			var orderDetail = new List<OrderDetailViewModel>();
			if (orderDetails.Any())
			{
				foreach (var order in orderDetails)
				{
					var productInfo = await _context.Products.Where(p => p.Id == order.ProductId).SingleOrDefaultAsync();
					var category = await _context.Categories.Where(c => c.Id == productInfo.CategoryId).FirstOrDefaultAsync();
					var result = new OrderDetailViewModel
					{
						Id = order.Id,
						OrderId = orderId,
						Name = productInfo.Name,
						Price = productInfo.Price,
						Quantity = order.Quantity,
						Category = category.Name,
						ProductImgs = productInfo.ThumbPath,
					};
					orderDetail.Add(result);
				}
				return orderDetail;
			}

			return new List<OrderDetailViewModel>();
		}

		public async Task<bool> CancleOrder(int orderId)
		{
			var order = await _context.Orders.Where(o => o.Id == orderId).SingleOrDefaultAsync();
			if (order != null)
			{
				order.isCancle = true;
				_context.SaveChanges();
				return true;
			}

			return false;
		}
		
        private async Task<bool> CreateOrderDetail(decimal price, int orderId, int productId, int quantity)
		{
				var orderDetail = new OrderDetail()
				{
					OrderId = orderId,
					ProductId = productId,
					Price = price,
					Quantity = quantity

				};
				_context.OrderDetails.Add(orderDetail);
				await _context.SaveChangesAsync();
			return true;
		}

		private async Task<bool> DeleteCart(string userId)
		{
			var cart = _context.Carts.Where(c => c.UserId.ToString() == userId).ToList();
			_context.Carts.RemoveRange(cart);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}

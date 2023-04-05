﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Order
{
	public class OrderService : IOrderService
	{
		private readonly OShopDbContext _context;
		public OrderService(OShopDbContext context)
		{
			_context = context;
		}

		public async Task<int> CreateOrder(InfoCustomerModel model)
		{
			var order = new Data.Entities.Order()
			{
				OrderDate = DateTime.Now,
				UserId = new Guid(model.UserId),
				Email = model.Email,
				Address = model.Address,
				PhoneNumber = Convert.ToInt32(model.PhoneNumber),
				City = model.City,
				District = model.District,
				Ward = model.Ward,
				PostCode = model.PostCode,
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

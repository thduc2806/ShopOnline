using Microsoft.AspNetCore.Components.Forms;
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

		public async Task<bool> CreateOrder(OrderModel model)
		{
			var order = new Data.Entities.Order()
			{
				OrderDate = DateTime.Now,
				UserId = new Guid(model.UserId),
				Amount = Convert.ToDecimal(model.Amount),
				CurrencyCode = model.CurrencyCode,
				PaymentId = model.PaymentId
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			if (model.Items.Count > 0)
			{
				foreach (var item in model.Items)
				{
					await CreateOrderDetail(item.Price, order.Id, item.ProductId, item.Quantity);
				}
			}
			await DeleteCart(model.UserId);

			return true;
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

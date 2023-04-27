using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Order
{
	public interface IOrderService
	{
		Task<int> CreateOrder(InfoCustomerModel model);

		Task<bool> UpdatePayment(OrderModel model);

		Task<PageResult<OrderViewModel>> GetOrder(GetOrderModel request);

		Task<PageResult<OrderViewModel>> GetById(GetOrderByIdModel request);

		Task<List<OrderDetailViewModel>> GetOrderDetail(int orderId);

		Task<OrderViewModel> GetByOrderId(int orderId);

		Task<bool> CancleOrder(int orderId);

	}
}

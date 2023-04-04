using oShopSolution.ViewModels.Catalog.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Order
{
	public interface IOrderService
	{
		Task<bool> CreateOrder(InfoCustomerModel model);

		Task<bool> UpdatePayment(OrderModel model);

    }
}

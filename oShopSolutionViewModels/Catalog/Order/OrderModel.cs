using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
	public class OrderModel
	{
		public DateTime OrderDate { set; get; }

		public string UserId { get; set; }

		public string Amount { get; set; }

		public string CurrencyCode { get; set; }

		public string PaymentId { get; set; }

		public List<OrderDetailView> Items { get; set; }
	}
}

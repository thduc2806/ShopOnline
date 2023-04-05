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

        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }

        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PostCode { get; set; }
        public int OrderId { get; set; }

        public List<OrderDetailView> Items { get; set; }
	}
}

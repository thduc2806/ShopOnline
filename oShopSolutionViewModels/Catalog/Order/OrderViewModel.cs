using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public DateTime OrderDate { get; set; }

		public decimal Amount { get; set; }

		public bool isPayment { get; set; }

		public bool isCancle { get; set; }
		
		public string Address { get; set; }

		public string City { get; set; }

		public string District { get; set; }

		public string Ward { get; set; }

		public int PhoneNumber { get; set; }

		public string FullName { get; set; }
	}
}

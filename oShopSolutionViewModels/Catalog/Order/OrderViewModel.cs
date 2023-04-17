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
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
	public class OrderDetailView
	{
		public int OrderId { set; get; }

		public int ProductId { set; get; }

		public int Quantity { set; get; }

		public decimal Price { set; get; }
	}
}

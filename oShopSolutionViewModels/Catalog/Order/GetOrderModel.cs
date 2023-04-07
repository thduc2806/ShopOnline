using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
	public class GetOrderModel : PageRequestBase
	{
		public string Keyword { get; set; }

		public string SortBy { get; set; }

		public string SortDir { get; set; }
	}
}

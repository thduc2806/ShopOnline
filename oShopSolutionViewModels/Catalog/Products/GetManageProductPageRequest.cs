using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class GetManageProductPageRequest : PageRequestBase
	{
		public string Keyword { get; set; }
		public List<int> CategoryIds { get; set; }
	}
}

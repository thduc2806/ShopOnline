using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class GetManageProductPageRequest
	{
		public string Keyword { get; set; }
		public int? CategoryId { get; set; }
	}
}

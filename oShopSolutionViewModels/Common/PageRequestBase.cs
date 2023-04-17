using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Common
{
	public class PageRequestBase
	{
		public int PageIndex { get; set; } = 1;

		public int PageSize { get; set; } = 5;
		

	}
}

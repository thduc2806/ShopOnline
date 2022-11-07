using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Common
{
	public class PageResultBase
	{
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                var pageCount = (double)TotalItems / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}

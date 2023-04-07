using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Dashboard
{
    public class TotalProductViewModel
    {
        public List<PercentProduct> PercentProducts { get; set; }

        public int TotalProduct { get; set; }

    }

    public class PercentProduct
    {
        public float Percent { get; set; }
    }
}

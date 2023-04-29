using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Order
{
    public class GetOrderByIdModel : PageRequestBase
    {
        public string UserId { get; set; }
    }
}

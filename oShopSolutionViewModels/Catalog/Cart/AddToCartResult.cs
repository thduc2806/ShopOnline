using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Cart
{
    public class AddToCartResult
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool Success { get; set; }

        public int TotalItems { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ThumbImage { get; set; }
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public int TotalsItem { get; set; }

        public int ProductId { get; set; }
    }
}

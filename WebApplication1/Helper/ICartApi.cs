using oShopSolution.ViewModels.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public interface ICartApi
    {
        Task<List<CartViewModel>> GetCart(string userId);
    }
}

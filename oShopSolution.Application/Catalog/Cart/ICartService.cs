using oShopSolution.ViewModels.Catalog.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Cart
{
    public interface ICartService
    {
        Task<AddToCartResult> AddToCart(CartModel model);

        Task<AddToCartResult> AddToCart(string customerId, string createdById, int productId, int quantity);
        Task<List<CartViewModel>> GetCart(string userId);

        Task<int> GetTotalProduct(string userId);

        Task<bool> UpdateQuantity(CartModel model);

	}
}

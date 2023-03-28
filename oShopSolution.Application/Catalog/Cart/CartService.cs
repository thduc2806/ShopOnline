using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Cart
{
    public class CartService : ICartService
    {
        private readonly OShopDbContext _context;

        public CartService(OShopDbContext context)
        {
            _context = context;
        }

        public async Task<AddToCartResult> AddToCart(CartModel model)
        {
            return await AddToCart(model.CustomerId, model.CustomerId, model.ProductId, model.Quantity);
        }

        public async Task<AddToCartResult> AddToCart(string customerId, string createdById, int productId, int quantity)
        {
            var addToCartResult = new AddToCartResult { Success = false };

            if (quantity <= 0)
            {
                addToCartResult.ErrorMessage = "The quantity must be larger than zero";
                addToCartResult.ErrorCode = "wrong-quantity";
                return addToCartResult;
            }

            var cart = await _context.Carts.Where(x => x.UserId == new Guid(customerId)).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Data.Entities.Cart
                {
                    UserId = new Guid(customerId),
                    ProductId = productId,
                    Quantity = quantity,
                    DateCreated = DateTime.Now,
                };

                await _context.AddAsync(cart);
            }
            else
            {
                var cartProduct = await _context.Carts.Where(p => p.ProductId == productId && p.UserId.ToString() == customerId).FirstOrDefaultAsync();
                if(cartProduct == null)
                {
                    cart = new Data.Entities.Cart
                    {
                        UserId = new Guid(customerId),
                        ProductId = productId,
                        Quantity = quantity,
                        DateCreated = DateTime.Now,
                    };
                    await _context.AddAsync(cart);
                }
                else
                {
                    cartProduct.Quantity = cartProduct.Quantity + quantity;
                    cartProduct = await _context.Carts.FirstOrDefaultAsync(x => x.Id == cartProduct.Id);
                }  
            }

            await _context.SaveChangesAsync();

            addToCartResult.Success = true;
            addToCartResult.TotalItems = await _context.Carts.Where(x => x.UserId.ToString() == customerId).CountAsync();
            return addToCartResult;
        }

        public async Task<List<CartViewModel>> GetCart(string userId)
        {
            var carts = await _context.Carts.Where(c => c.UserId.ToString() == userId).ToListAsync();
            if (carts.Any())
            {
                var results = new List<CartViewModel>();
                foreach (var cart in carts)
                {
                    var productInfo = await _context.Products.Where(p => p.Id == cart.ProductId).FirstOrDefaultAsync();

                    var result = new CartViewModel{
                        UserId = userId,
                        Name = productInfo.Name,
                        Price = productInfo.Price,
                        Id = cart.Id,
                        Quantity = cart.Quantity,
                        ThumbImage = productInfo.ThumbPath,
                        TotalsItem = carts.Count
                    };
                    results.Add(result);
                }
                return results;
            }
            return new List<CartViewModel>();
        }

        public async Task<int> GetTotalProduct(string userId)
        {
            var total = await _context.Carts.Where(x => x.UserId.ToString() == userId).CountAsync();
            return total;
        }
    }
}

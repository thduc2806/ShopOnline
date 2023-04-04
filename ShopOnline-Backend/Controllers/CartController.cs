using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Cart;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartModel model)
        {
            var result = await _cartService.AddToCart(model);
            return Ok(result);
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCart(userId);
            return Ok(cart);
        }

        [HttpGet("TotalItem")]
        public async Task<IActionResult> GetTotalProduct(string userId)
        {
            var result = await _cartService.GetTotalProduct(userId);
            return Ok(result);
        }

        [HttpPut("updateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartModel model)
        {
            var result = await _cartService.UpdateQuantity(model);
            return Ok(result);
        }

	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Cart;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartApi _cartApi;
        public CartController(ICartApi cartApi)
        {
            _cartApi = cartApi;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            var result = await _cartApi.GetCart(userId);
			decimal totalPrice = result.Sum(x => x.Price * x.Quantity);
			ViewBag.TotalPrice = totalPrice;
			return View(result);
		}
    }
}

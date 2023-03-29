using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers.Component
{
    public class MiniCartViewComponent : ViewComponent
    {
		private readonly ICartApi _cartApi;
		public MiniCartViewComponent(ICartApi cartApi)
		{
			_cartApi = cartApi;
		}
        public async Task<IViewComponentResult> InvokeAsync(string userId)
		{
			var result = await _cartApi.GetCart(userId);
			decimal totalPrice = result.Sum(x => x.Price * x.Quantity);
			ViewBag.TotalPrice = totalPrice;
			return View("MiniCart", result);
		}
	}
}

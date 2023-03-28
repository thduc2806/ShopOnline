using Microsoft.AspNetCore.Mvc;
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
			return View("MiniCart", result);
		}
	}
}

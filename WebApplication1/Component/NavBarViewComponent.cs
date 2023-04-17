using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Component
{
    public class NavBarViewComponent : ViewComponent
    {
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("NavBar");
		}
	}
}

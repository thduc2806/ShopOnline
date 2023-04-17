using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Component
{
    public class FilterMediaViewComponent : ViewComponent
    {
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("FilterMedia");
		}
	}
}

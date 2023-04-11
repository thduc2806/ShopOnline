using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers.Component
{
    public class FilterViewComponent : ViewComponent
    {
		private readonly ICategoryAPI _categoriAPI;
		public FilterViewComponent (ICategoryAPI categoryAPI)
		{
			_categoriAPI = categoryAPI;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var result = await _categoriAPI.GetAll();
			return View("Filter", result);
		}

	}
}

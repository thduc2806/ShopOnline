using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers.Component
{
	public class SideBarViewComponent : ViewComponent
	{
		private readonly ICategoryAPI _categoryAPI;
		public SideBarViewComponent(ICategoryAPI categoryAPI)
		{
			_categoryAPI = categoryAPI;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = await _categoryAPI.GetAll();
			return View(items);
		}
	}
}

﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Component
{
    public class ToolBarViewComponent : ViewComponent
    {
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("ToolBar");
		}
	}
}

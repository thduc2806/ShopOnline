using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var category = await _categoryService.GetAll();
			return Ok(category);
		}
	}
}

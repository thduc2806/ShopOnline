using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductAPI _productAPI;

		public ProductController(IProductAPI productAPI)
		{
			_productAPI = productAPI;
		}
		public async Task<IActionResult> Detail(int id)
		{
			var product = await _productAPI.GetById(id);
			return View(new ProductDetailViewModel()
			{
				Product = product,
			});
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Products;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductAPI _productApi;
		private readonly ICategoryAPI _categoryApi;


		public HomeController(IProductAPI productApi, ICategoryAPI categoryApi)
		{
			_productApi = productApi;
			_categoryApi = categoryApi;
		}

		public async Task<IActionResult> Index(int pageIndex = 1)
		{
			//var pageIndex = 1;
			var pageSize = 12;
			var request = new GetManageProductPageRequest()
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
			};
			var product = await _productApi.GetAllProduct(request);
			return View(product);
		}

		//[HttpPost]
		//public async Task<IActionResult> Index(int pageIndex)
		//{
		//	int pageSize = 1;
		//	var request = new GetManageProductPageRequest()
		//	{
		//		PageIndex = pageIndex,
		//		PageSize = pageSize,
		//	};

		//	var product = await _productApi.GetAllProduct(request);

		//	return View(product);
		//}

		[HttpGet]
		public async Task<IActionResult> Detail(int id)
		{
			var result = await _productApi.GetProductById(id);
			return PartialView(result);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

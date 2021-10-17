using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Products;
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

		public async Task<IActionResult> Index()
		{
			var viewModel = new HomeViewModels 
			{
				Product =  await _productApi.GetAll(),
				Category = await _categoryApi.GetAll()

			};
			return View(viewModel);
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

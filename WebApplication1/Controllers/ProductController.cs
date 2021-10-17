using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.Catalog.Products;
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
		private readonly ICategoryAPI _categoryAPI;


		public ProductController(IProductAPI productAPI, ICategoryAPI categoryAPI)
		{
			_productAPI = productAPI;
			_categoryAPI = categoryAPI;
		}
		public async Task<IActionResult> Detail(int id)
		{
			var product = await _productAPI.GetById(id);
			return View(new ProductDetailViewModel()
			{
				Product = product,
			});
		}

		public async Task<IActionResult> Category(int id)
		{
			var product = await _productAPI.GetAllPagings(new GetManageProductPageRequest()
			{
				CategoryId = id,
			});
			return View(new ProductCategoryViewModel() 
			{
				Products = product
			});
		}
	}
}

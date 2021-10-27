using Microsoft.AspNetCore.Mvc;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Comments;
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
		private readonly ICommentAPI _commentAPI;
		private readonly OShopDbContext _context;


		public ProductController(IProductAPI productAPI, ICategoryAPI categoryAPI, ICommentAPI commentAPI, OShopDbContext context)
		{
			_productAPI = productAPI;
			_categoryAPI = categoryAPI;
			_commentAPI = commentAPI;
			_context = context;

		}
		public async Task<IActionResult> Detail(int id)
		{
			var product = await _productAPI.GetById(id);
			ViewBag.product = product;
			var comment = new CommentView()
			{
				ProductId = id,
			};
			return View("Detail", comment);
		}
		[HttpPost]
		public ActionResult SendReview(CommentRequest request, int rating, int productId)
		{
			request.Rating = rating;
			_commentAPI.Create(request);
			_context.SaveChanges();
			return RedirectToAction("Detail", "Product", new { id = request.ProductId });
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

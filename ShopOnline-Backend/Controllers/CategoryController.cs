using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Category;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Categories;
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
		private readonly OShopDbContext _context;
		public CategoryController(ICategoryService categoryService, OShopDbContext context)
		{
			_categoryService = categoryService;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var category = await _categoryService.GetAll();
			return Ok(category);
		}

		[HttpGet("{id}")]
		public Category Get(int id)
		{
			var category = _context.Categories.Select(s => new Category
			{
				Id = s.Id,
				Name = s.Name,
				Description = s.Description
			}).Where(a => a.Id == id).FirstOrDefault();
			return category;
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)

		{
			var category = _context.Categories.Find(id);
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] CategoryUpdateRequest request)
		{
			var category = _context.Categories.Find(id);
			category.Name = request.Name;
			category.Description = request.Description;
			await _context.SaveChangesAsync();
			return Ok(1);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CategoryUpdateRequest request)
		{
			var cate = new Category()
			{
				Name = request.Name,
				Description = request.Description
			};
			_context.Categories.Add(cate);
			await _context.SaveChangesAsync();
			if (cate.Id > 0)
			{
				return Ok();
			}
			return BadRequest();
		}


	}
}

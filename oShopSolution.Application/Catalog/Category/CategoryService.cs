using Microsoft.EntityFrameworkCore;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly OShopDbContext _context;
		public CategoryService(OShopDbContext context)
		{
			_context = context;
		}
		public async Task<List<CategoryView>> GetAll()
		{
			var c = _context;
			var category = await c.Categories.Select(c => new CategoryView()
			{
				Id = c.Id,
				Name = c.Name,
			}).ToListAsync();
			return category;
		}
	}
}

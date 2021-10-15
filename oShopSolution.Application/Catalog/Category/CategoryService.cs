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
			var query = from c in _context.Categories
						select new { c };
			var category = await query.Select(x => new CategoryView()
			{
				Id = x.c.Id,
				Name = x.c.Name,
				Description = x.c.Description
			}).ToListAsync();
			return category;
		}

		public async Task<List<CategoryView>> GetById(int id)
		{
			var query = from c in _context.Categories
						where c.Id == id
						select new { c };
			var category = await query.Select(x => new CategoryView()
			{
				Id = x.c.Id,
				Name = x.c.Name,
				Description = x.c.Description
			}).ToListAsync();
			return category;
		}
	}
}

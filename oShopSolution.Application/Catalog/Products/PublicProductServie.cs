using Microsoft.EntityFrameworkCore;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Products
{
	public class PublicProductServie : IPublicProductService
	{
		private readonly OShopDbContext _context;
		public PublicProductServie(OShopDbContext context)
		{
			_context = context;
		}

		public async Task<List<ProductView>> GetAll()
		{
			var p = _context;
			var product = await p.Products.Select(s => new ProductView()
			{
				Id = s.Id,
				Name = s.Name,
				Price = s.Price,
				Description = s.Description,
				Rating = s.Rating,
				CreateDate = s.CreateDate,
			}).ToListAsync();
			return product;
		}

		//public async Task<List<ProductView>> GetAll()
		//{
		//	var query = from p in _context.Products
		//				join pic in _context.ProductInCategories on p.Id equals pic.ProductId
		//				join c in _context.Categories on pic.CategoryId equals c.Id
		//				select new { p, pic };
		//	var data = await query.Select(x => new ProductView()
		//		{
		//			Id = x.p.Id,
		//			Name = x.p.Name,
		//			Description = x.p.Description,
		//			Price = x.p.Price,
		//			Rating = x.p.Rating,
		//			CreateDate = x.p.CreateDate,
		//		}).ToListAsync();
		//	return data;
		//}

		public async Task<PageResult<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request)
		{
			var query = from p in _context.Products
						join pic in _context.ProductInCategories on p.Id equals pic.ProductId
						join c in _context.Categories on pic.CategoryId equals c.Id
						select new { p, pic };
			if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
			{
				query = query.Where(p => p.pic.CategoryId == request.CategoryId);
			}
			int totalRow = await query.CountAsync();
			var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
				.Take(request.PageSize)
				.Select(x => new ProductView()
				{
					Id = x.p.Id,
					Name = x.p.Name,
					Description = x.p.Description,
					Price = x.p.Price,
					Rating = x.p.Rating,
					CreateDate = x.p.CreateDate,
				}).ToListAsync();
			var pageResult = new PageResult<ProductView>()
			{
				TotalRecord = totalRow,
				Items = data,
			};
			return pageResult;
		}
	}
}

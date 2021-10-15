using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using oShopSolution.Application.Common;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.Utilities.Exceptions;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Products
{
	public class ManageProductServie : IManageProductService
	{
		private readonly OShopDbContext _context;
		private readonly IStorageService _storageService;
		public ManageProductServie(OShopDbContext context, IStorageService storageService)
		{
			_context = context;
			_storageService = storageService;
		}
		public async Task<int> Create(ProductCreateRequest request)
		{
			var product = new Product()
			{
				Name = request.Name,
				Description = request.Description,
				Rating = request.Rating,
				Price = request.Price,
				CreateDate = DateTime.Now,
				CategoryId = request.CategoryId,
				ThumbPath = request.ThumbPath,
				
			};
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return product.Id; 
		}

		public async Task<int> Delete(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null) throw new OShopException($"Cannot find product: {productId}");
			_context.Products.Remove(product);
			return await _context.SaveChangesAsync();
		}


		public async Task<PageResult<ProductView>> GetAllPaging(GetManageProductPageRequest request)
		{
			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.Id into pc
						from c in pc.DefaultIfEmpty()
						select new { p, pc };
			if (!string.IsNullOrEmpty(request.Keyword))
				query = query.Where(x => x.p.Name.Contains(request.Keyword));
			if (request.CategoryId != null && request.CategoryId != 0)
			{
				query = query.Where(p => p.p.CategoryId == request.CategoryId);
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
					CategoryId = x.p.CategoryId,
					CreateDate = x.p.CreateDate,
					ThumbPath = x.p.ThumbPath,
				}).ToListAsync();
			var pageResult = new PageResult<ProductView>()
			{
				TotalRecord = totalRow,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				Items = data
			};
			return pageResult;
		}

		public async Task<ProductView> GetById(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			var category = await (from c in _context.Categories
							join p in _context.Products on c.Id equals p.CategoryId
							where p.Id == productId
							select c.Name).ToListAsync();
			var productView = new ProductView()
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Rating = product.Rating,
				Description = product.Description,
				CreateDate = product.CreateDate,
				ThumbPath = product.ThumbPath,
				CategoryId = product.CategoryId,
				Category = category,

			};
			return productView;
		}

		public async Task<int> Update(ProductUpdateRequest request)
		{
			var product = await _context.Products.FindAsync(request.Id);
			if (product == null) throw new OShopException($"Can not find witd id : {request.Id}");

			product.Name = request.Name;
			product.Description = request.Description;
			product.Rating = request.Rating;
			product.ThumbPath = request.ThumbPath;

			return await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdatePrice(int producId, decimal newPrice)
		{
			var product = await _context.Products.FindAsync(producId);
			if (product == null) throw new OShopException($"Can not find witd id : {producId}");
			product.Price = newPrice;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<PageResult<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request)
		{
			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.Id into pc
						from c in pc.DefaultIfEmpty()
						select new { p, c };
			if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
			{
				query = query.Where(p => p.c.Id== request.CategoryId);
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
					CategoryId = x.p.CategoryId,
					CreateDate = x.p.CreateDate,
					ThumbPath = x.p.ThumbPath
				}).ToListAsync();
			var pageResult = new PageResult<ProductView>()
			{
				TotalRecord = totalRow,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				Items = data,
			};
			return pageResult;
		}

		public async Task<List<ProductView>> GetAll()
		{
			var query = from p in _context.Products
						 select new { p };

			var product = await query.Select(z => new ProductView()
			{
				Id = z.p.Id,
				Name = z.p.Name,
				Description = z.p.Description,
				Price = z.p.Price,
				CreateDate = z.p.CreateDate,
				CategoryId = z.p.CategoryId,
				Rating = z.p.Rating,
				ThumbPath = z.p.ThumbPath,
			}).ToListAsync();
			return product;
		}
		
	}
}

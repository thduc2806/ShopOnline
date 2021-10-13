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
				
			};
			//IMG
			if(request.ThumbImg != null)
			{
				product.ProductImgs = new List<ProductImg>()
				{
					new ProductImg()
					{
						FileSize = request.ThumbImg.Length,
						ImgPath = await this.SaveFile(request.ThumbImg),
						
					}
				};
			}
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return product.Id; 
		}

		public async Task<int> Delete(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null) throw new OShopException($"Cannot find product: {productId}");
			var imges = _context.ProductImgs.Where(i => i.ProductId == productId);
			foreach(var imge in imges)
			{
				await _storageService.DeleteFileAsync(imge.ImgPath);
			}
			_context.Products.Remove(product);
			return await _context.SaveChangesAsync();
		}


		public async Task<PageResult<ProductView>> GetAllPaging(GetManageProductPageRequest request)
		{
			var query = from p in _context.Products
						join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
						from pic in ppic.DefaultIfEmpty()
						join c in _context.Categories on pic.CategoryId equals c.Id into picc
						from c in picc.DefaultIfEmpty()
						select new { p, pic };
			if (!string.IsNullOrEmpty(request.Keyword))
				query = query.Where(x => x.p.Name.Contains(request.Keyword));
			if (request.CategoryIds.Count > 0)
			{
				query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
			}
			int totalRow = await query.CountAsync();
			var data =  await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
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

		public async Task<ProductView> GetById(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			var category = await (from c in _context.Categories
							join pic in _context.ProductInCategories on c.Id equals pic.CategoryId
							where pic.ProductId == productId
							select c.Name).ToListAsync();
			var productView = new ProductView()
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Description = product.Description,
				CreateDate = product.CreateDate,
				Category = category
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

			if (request.ThumbImg != null)
			{
				var thumbImg = await _context.ProductImgs.FirstOrDefaultAsync(i => i.ProductId == request.Id);
				if (thumbImg != null)
				{
					thumbImg.FileSize = request.ThumbImg.Length;
					thumbImg.ImgPath = await this.SaveFile(request.ThumbImg);

					_context.ProductImgs.Update(thumbImg);

				}
			}

			return await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdatePrice(int producId, decimal newPrice)
		{
			var product = await _context.Products.FindAsync(producId);
			if (product == null) throw new OShopException($"Can not find witd id : {producId}");
			product.Price = newPrice;
			return await _context.SaveChangesAsync() > 0;
		}
		private async Task<string> SaveFile(IFormFile file)
		{
			var orFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(orFileName)}";
			await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
			return fileName;
		}


		public async Task<PageResult<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request)
		{
			var query = from p in _context.Products
						join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
						from pic in ppic.DefaultIfEmpty()
						join c in _context.Categories on pic.CategoryId equals c.Id into picc
						from c in picc.DefaultIfEmpty()
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
	}
}

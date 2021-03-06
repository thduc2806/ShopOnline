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
		private const string USER_CONTENT_FOLDER_NAME = "user-content";
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
				Price = request.Price,
				CreateDate = DateTime.Now,
				CategoryId = request.CategoryId,
				
			};
			if (request.ThumbImg != null)
			{
				product.ProductImgs = new List<ProductImg>()
				{
					new ProductImg()
					{
						FileSize = request.ThumbImg.Length,
						ImgPath = await this.SaveFile(request.ThumbImg),
						IsDefault = true,
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
			var images = _context.ProductImgs.Where(i => i.ProductId == productId);
			foreach (var image in images)
			{
				await _storageService.DeleteFileAsync(image.ImgPath);
			}
			_context.Products.Remove(product);
			return await _context.SaveChangesAsync();
		}


		public async Task<List<ProductView>> GetAllPagings(GetManageProductPageRequest request)
		{
			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.Id into pc
						from c in pc.DefaultIfEmpty()
						join pi in _context.ProductImgs on p.Id equals pi.ProductId into ppi
						from pi in ppi.DefaultIfEmpty()
						where pi.IsDefault == true
						select new { p, pc, pi,c };
			if (!string.IsNullOrEmpty(request.Keyword))
				query = query.Where(x => x.p.Name.Contains(request.Keyword));
			if (request.CategoryId != null && request.CategoryId != 0)
			{
				query = query.Where(p => p.p.CategoryId == request.CategoryId);
			}
			var data = await query.Select(x => new ProductView()
				{
					Id = x.p.Id,
					Name = x.p.Name,
					Description = x.p.Description,
					Price = x.p.Price,
					Rating = x.p.Rating,
					CategoryId = x.p.CategoryId,
					CreateDate = x.p.CreateDate,
					Category = x.c.Name,
					ThumbImg = x.pi.ImgPath
					
				}).ToListAsync();
			return data;
		}

		public async Task<ProductView> GetById(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			var comment = await _context.ProductComments.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
			var category = await _context.Categories.Where(x=> product.CategoryId == x.Id).FirstOrDefaultAsync();
			var img = await _context.ProductImgs.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
			var productView = new ProductView()
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Rating = comment != null ? comment.Rating : 0,
				Description = product.Description,
				CreateDate = product.CreateDate,
				CategoryId = product.CategoryId,
				Category = category.Name,
				TextComment = comment != null ? comment.TextComment : "",
				ThumbImg = img != null ? img.ImgPath : "no-img-jpg"
			};
			return productView;
		}

		public async Task<int> Update(ProductUpdateRequest request, int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null) throw new OShopException($"Can not find witd id : {id}");

			product.Name = request.Name;
			product.Description = request.Description;
			product.Price = request.Price;

			return await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdatePrice(int producId, decimal newPrice)
		{
			var product = await _context.Products.FindAsync(producId);
			if (product == null) throw new OShopException($"Can not find witd id : {producId}");
			product.Price = newPrice;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<List<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request)
		{
			var query = from p in _context.Products
						join pi in _context.ProductImgs on p.Id equals pi.ProductId
						join c in _context.Categories on p.CategoryId equals c.Id
						select new { p, c, pi};
			if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
			{
				query = query.Where(p => p.c.Id== request.CategoryId);
			}
			var data = await query.Select(x => new ProductView()
				{
					Id = x.p.Id,
					Name = x.p.Name,
					Description = x.p.Description,
					Price = x.p.Price,
					Rating = x.p.Rating,
					CategoryId = x.p.CategoryId,
					CreateDate = x.p.CreateDate,
					ThumbImg = x.pi.ImgPath
				}).ToListAsync();
			return data;
		}

		public async Task<List<ProductView>> GetAll()
		{
			var query = from p in _context.Products
						join pi in _context.ProductImgs on p.Id equals pi.ProductId into ppi
						from pi in ppi.DefaultIfEmpty()
						join c in _context.Categories on p.CategoryId equals c.Id into pc
						from c in pc.DefaultIfEmpty()
						where (pi == null || pi.IsDefault == true)
						select new { p, c, pi};

			var product = await query.OrderByDescending(z => z.p.CreateDate) .Select(z => new ProductView()
			{
				Id = z.p.Id,
				Name = z.p.Name,
				Description = z.p.Description,
				Price = z.p.Price,
				CreateDate = z.p.CreateDate,
				CategoryId = z.p.CategoryId,
				Rating = z.p.Rating,
				Category = z.c.Name,
				ThumbImg = z.pi.ImgPath,
			}).ToListAsync();
			return product;
		}

		private async Task<string> SaveFile(IFormFile file)
		{
			var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
			await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
			return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
		}

	}
}

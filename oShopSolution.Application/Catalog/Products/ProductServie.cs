using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using oShopSolution.Application.Common;
using oShopSolution.Application.Option;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.Utilities.Constants;
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
	public class ProductServie : IProductService
	{
		private readonly OShopDbContext _context;
        private readonly ImageConfigOption _config;
		private readonly IStorageService _storageService;
		private readonly string _userContentFolder;
		private const string USER_CONTENT_FOLDER_NAME = "App_Data";

		public ProductServie(OShopDbContext context, IOptions<ImageConfigOption> options, IWebHostEnvironment webHostEnvironment, IStorageService storageService)
		{
			_context = context;
			_config = options.Value;
			_userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, "App_Data");
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
				Rating = 10,		
			};
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

            if (request.ThumbImg != null)
            {
				var imagePath = await this.SaveFile(request.ThumbImg);
                product.ProductImgs = new List<ProductImg>()
                {
                    new ProductImg()
                    {
                        FileSize = request.ThumbImg.Length,
                        ImgPath = imagePath,
                        IsDefault = true,
                        ProductId = product.Id,
                    }
                };
				product.ThumbPath = imagePath;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            return product.Id; 
		}

		public async Task<int> Delete(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null) throw new OShopException($"Cannot find product: {productId}");
			var images = _context.ProductImgs.Where(i => i.ProductId == productId);
			foreach (var image in images)
			{
			}
			_context.Products.Remove(product);
			return await _context.SaveChangesAsync();
		}


		public async Task<PageResult<ProductView>> GetAllPagings(GetManageProductPageRequest request)
		{

			var query = from p in _context.Products
						join c in _context.Categories on p.CategoryId equals c.Id into pc
						from c in pc.DefaultIfEmpty()
						select new { p, pc, c };

			if (request.CateId > 0)
			{
				query = query.Where(o => o.p.CategoryId == request.CateId);
			}	

			int total = await query.CountAsync();



			if(!string.IsNullOrEmpty(request.Keyword))
			{
				query = query.Where(c => c.p.Name.Contains(request.Keyword));
			}

			if(request.SortBy != null)
			{
				switch(request.SortBy)
				{
					case SortingProductConstant.Name:
                        query = query.OrderByDescending(c => c.p.Name);
						break;
					case SortingProductConstant.Price:
						query = query.OrderByDescending(c => c.p.Price);
						break;

					default: query = query.OrderByDescending(c => c.p.CreateDate);
						break;
                }
			}
			else
			{
				query = query.OrderByDescending(c => c.p.CreateDate);
			}

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new ProductView()
			{
				Id = x.p.Id,
				Name = x.p.Name,
				Description = x.p.Description,
				Price = x.p.Price,
				Category = x.c.Name,
				CreateDate = x.p.CreateDate,
				ThumbImg = x.p.ThumbPath,
				CategoryId = x.p.CategoryId,
			}).ToListAsync();

			var pageResult = new PageResult<ProductView>
			{
				TotalItems = total,
				PageSize = request.PageSize,
				PageIndex = request.PageIndex,
				Items = data,
				CateId = request.CateId
			};
			return pageResult;
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
				//TextComment = comment != null ? comment.TextComment : "",
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

		public async Task<PageResult<ProductView>> GetAllPagingByCateId(int cateId, GetManageProductPageRequest request)
        {
			var query = from p in _context.Products
						join pi in _context.ProductImgs on p.Id equals pi.ProductId
						join c in _context.Categories on p.CategoryId equals c.Id
						where p.CategoryId == cateId
						select new { p, pi, c };

			int total = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new ProductView()
			{
				Id = x.p.Id,
				Name = x.p.Name,
				Description = x.p.Description,
				Price = x.p.Price,
				CreateDate = x.p.CreateDate,
				Category = x.c.Name,
				ThumbImg = x.pi.ImgPath,
			}).ToListAsync();

			var pageResult = new PageResult<ProductView>()
			{
				TotalItems = total,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				Items = data,
				CateId = request.CateId,
			};

			return pageResult;
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

        public async Task<ProductImg> ImportFileAsync(IFormFile file, int producId)
		{

			string savefolder = Path.Combine(_config.ImagePath, DateTime.Now.ToString("yyyy-MM-dd"));
			string folderPath = Path.Combine(_userContentFolder, DateTime.Now.ToString("yyyy-MM-dd"));

            // if the folder does not exist then create new
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileExtension = Path.GetExtension(file.FileName);

            var imgProduct = new ProductImg
            {
				ProductId = producId,
				IsDefault = true,
                ImgPath = Path.Combine(savefolder, producId.ToString() + fileExtension)
            };

            //TODO: clean the file if error
            using (var stream = new FileStream(imgProduct.ImgPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _context.Add(imgProduct);
            _context.SaveChanges();

            return imgProduct;
        }

		public bool IsExelFileAsync(IFormFile file)
		{
			var fileType = Path.GetExtension(file.FileName);

			if(!_config.ImageTypes.Any(x => x.Equals(fileType, StringComparison.OrdinalIgnoreCase)))
			{
				return false;
			}
			return true;
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


using Microsoft.AspNetCore.Http;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Products
{
	public interface IManageProductService
	{
		Task<ProductView> GetById(int productId);
		Task<int> Create(ProductCreateRequest request);
		Task<int> Update(ProductUpdateRequest request);
		Task<int> Delete(int producId);
		Task<bool> UpdatePrice(int producId, decimal newPrice);
		Task<List<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request);
		Task<List<ProductView>> GetAllPagings(GetManageProductPageRequest request);

		Task<List<ProductView>> GetAll();
		//Task<List<ProductView>> GetAll();
		//Task<PageResult<ProductView>> GetAllPaging(GetManageProductPageRequest request);
		//Task<int> AddImg(int productId, List<IFormFile> files);
		//Task<int> RemoveImg(int imageId);



	}
}

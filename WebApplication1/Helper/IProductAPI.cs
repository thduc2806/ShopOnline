using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public interface IProductAPI
    {
        Task<ProductView> GetById(int id);

		Task<PageResult<ProductView>> GetAllProduct(GetManageProductPageRequest request);
		Task<ProductView> GetProductById(int Id);
	}
}

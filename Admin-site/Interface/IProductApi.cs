﻿using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;

namespace Admin_site.Interface
{
    public interface IProductApi
    {
        Task<PageResult<ProductView>> GetAllProduct(GetManageProductPageRequest request);
        Task<ProductView> GetProductById(int Id);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<bool> DeleteProduct(int id);

	}
}

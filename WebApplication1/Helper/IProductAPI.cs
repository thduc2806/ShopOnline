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

        Task<List<ProductView>> GetAll();
        Task<List<ProductView>> GetAllPagings(GetManageProductPageRequest request);
    }
}

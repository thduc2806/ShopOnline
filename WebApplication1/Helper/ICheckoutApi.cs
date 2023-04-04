using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Products;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using oShopSolution.ViewModels.Catalog.Order;

namespace WebApplication1.Helper
{
    public interface ICheckoutApi
    {
        Task<bool> CreateOder(InfoCustomerModel request);
    }
}

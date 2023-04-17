using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.System.Users;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class CartApi : BaseApiClient , ICartApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CartApi(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    
        public async Task<List<CartViewModel>> GetCart(string userId)
        {
            string url = $"https://localhost:5001/api/Cart/GetCart?userId={userId}";
            var data = await GetAsync<List<CartViewModel>>(url);
            return data;
        }


    }
}

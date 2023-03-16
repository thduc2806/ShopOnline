using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public class ProductAPI : BaseApiClient, IProductAPI
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		public ProductAPI(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<ProductView> GetById(int id)
        {
            var data = await GetAsync<ProductView>($"/api/product/{id}");

            return data;
        }

		public async Task<PageResult<ProductView>> GetAllProduct(GetManageProductPageRequest request)
		{
			var data = await GetAsync<PageResult<ProductView>>(
				$"/api/product/page?pageIndex={request.PageIndex}" +
				$"&pageSize={request.PageSize}" + $"&keyword={request.Keyword}");

			return data;
		}

		public async Task<ProductView> GetProductById(int Id)
		{
			var data = await GetAsync<ProductView>($"/api/product/{Id}");
			return data;
		}
	}
}

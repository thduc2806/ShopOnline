using Admin_site.Interface;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;

namespace Admin_site.Service
{
    public class ProductApi : BaseApiService, IProductApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductApi(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
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

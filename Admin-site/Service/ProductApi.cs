using System.Net.Http.Headers;
using Admin_site.Interface;
using oShopSolution.Utilities.Constants;
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

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var content = new MultipartFormDataContent();

            if(request.ThumbImg != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbImg.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbImg.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                content.Add(bytes, "thumbnailImage", request.ThumbImg.FileName);
            }

            content.Add(new StringContent(request.Name.ToString()), "name");
            content.Add(new StringContent(request.Price.ToString()), "price");
            content.Add(new StringContent(request.Description.ToString()), "description");

            var response = await client.PostAsync($"/api/product/", content);
            return response.IsSuccessStatusCode;

        }
    }
}

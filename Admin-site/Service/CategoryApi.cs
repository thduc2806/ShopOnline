using System.Net.Http.Headers;
using System.Text;
using Admin_site.Service;
using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Categories;

namespace Admin_site.Interface;

public class CategoryApi : BaseApiService, ICategoryApi
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly HttpClient httpClient;
    
    public CategoryApi(IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        httpClient = new HttpClient();
    }
    

    public async Task<List<CategoryView>> GetAll()
    {
        var data = await GetAsync<List<CategoryView>>($"/api/category");
        return data;
    }
    
    public async Task<bool> Create(CategoryUpdateRequest request)
    {
        var sessions = _httpContextAccessor
            .HttpContext
            .Session
            .GetString(SystemConstants.AppSettings.Token);

        var client = _httpClientFactory.CreateClient();
        var url = "https://localhost:5001/api/Category";
        HttpMethodEnum method = HttpMethodEnum.POST;
        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        HttpResponseMessage response = null;
        if (request != null)
        {
            string json = JsonConvert.SerializeObject(request);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            response = await PostDataAsync(method, url, data);
        }
        else
        {
            response = await PostDataAsync(method, url, null);
        }
        string responseData = await response.Content.ReadAsStringAsync();
        var result = new bool();
        if (response.IsSuccessStatusCode)
        {
            return true;

        }
        return false;
    }
    private Task<HttpResponseMessage> PostDataAsync(HttpMethodEnum method, string url, HttpContent content)
    {
        switch (method)
        {
            case HttpMethodEnum.POST:
                return httpClient.PostAsync(url, content);
            case HttpMethodEnum.PUT:
                return httpClient.PutAsync(url, content);
            case HttpMethodEnum.DELETE:
                return httpClient.DeleteAsync(url);
        }
        return httpClient.PostAsync(url, content);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Products;
using System.IO;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using oShopSolution.ViewModels.Catalog.Order;
using Newtonsoft.Json;
using System.Text;
using System.Security.Policy;
using oShopSolution.Application.Helper;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Helper
{
    public class CheckoutApi : BaseApiClient, ICheckoutApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public CheckoutApi(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            httpClient = new HttpClient();
        }

        public async Task<int> CreateOder(InfoCustomerModel request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            var url = "https://localhost:5001/api/Order";
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
            var result = new int();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<int>(responseData);

            }
            else
            {
                result = JsonConvert.DeserializeObject<int>(responseData);
            }
            return result;

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
}

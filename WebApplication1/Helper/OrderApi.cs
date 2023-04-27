using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class OrderApi : IOrderApi
    {
        private readonly HttpClient httpClient;

        public OrderApi()
        {
            httpClient = new HttpClient();
        }

        public async Task<PageResult<OrderViewModel>> GetOrder(GetOrderByIdModel request)
        {
            string url = $"https://localhost:5001/api/order?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" + $"&userId={request.UserId}";

            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(url);
            string responseData = await response.Content.ReadAsStringAsync();
            var result = new PageResult<OrderViewModel>();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<PageResult<OrderViewModel>>(responseData);

            }
            else
            {
                result = new PageResult<OrderViewModel>();
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> GetOrderDetail(int orderId)
        {
            string url = $"https://localhost:5001/api/order/orderdetail/{orderId}";

            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(url);
            string responseData = await response.Content.ReadAsStringAsync();
            var result = new List<OrderDetailViewModel>();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<OrderDetailViewModel>>(responseData);

            }
            else
            {
                result = new List<OrderDetailViewModel>();
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

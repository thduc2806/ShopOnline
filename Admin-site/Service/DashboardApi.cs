using Admin_site.Interface;
using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Dashboard;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;

namespace Admin_site.Service
{
	public class DashboardApi : IDashboardApi
	{
		protected APIExcute _aPIExcute;
        private readonly HttpClient httpClient;

        public DashboardApi()
		{
			_aPIExcute = new APIExcute();
            httpClient = new HttpClient();
        }

		public async Task<DashboardViewModel> GetTotal()
		{
			string url = "https://localhost:5001/api/Dashboard";

            HttpMethodEnum method = HttpMethodEnum.POST;
            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(url);
            string responseData = await response.Content.ReadAsStringAsync();
            var result = new DashboardViewModel();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<DashboardViewModel>(responseData);

            }
            else
            {
                result = new DashboardViewModel();
            }
            return result;
        }

		public async Task<PageResult<OrderViewModel>> GetOrder(GetOrderModel request)
		{
			string url = $"https://localhost:5001/api/order/page?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" + $"&keyword={request.Keyword}" + $"&sortBy={request.SortBy}" + $"&sortDir={request.SortDir}";

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

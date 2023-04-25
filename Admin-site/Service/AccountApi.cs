using Admin_site.Interface;
using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;

namespace Admin_site.Service
{
    public class AccountApi : IAccountApi
    {
        private readonly HttpClient httpClient;
        public AccountApi()
        {
            httpClient = new HttpClient();
        }

        public async Task<PageResult<UserProfileViewModel>> GetUser(PageRequestBase request)
        {
            string url = $"https://localhost:44321/api/account?page={request.PageIndex}" +
                $"&pageSize={request.PageSize}";

            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(url);
            string responseData = await response.Content.ReadAsStringAsync();
            var result = new PageResult<UserProfileViewModel>();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<PageResult<UserProfileViewModel>>(responseData);

            }
            else
            {
                result = new PageResult<UserProfileViewModel>();
            }
            return result;
        }
    }
}

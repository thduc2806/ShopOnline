using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class UserAPI : IUserAPI
	{
        protected APIExcute _aPIExcute;
        private readonly HttpClient httpClient;
        public UserAPI()
		{
            _aPIExcute = new APIExcute();
            httpClient = new HttpClient();
        }

        public async Task<AuthenViewModel> Authenticate(AuthenModel request)
        {
            string url = "https://localhost:44321/api/auth";

            var req = new BaseRequest<object>(new
            {
                Email = request.Email,
                request.Password
            });
            var res = await _aPIExcute.PostData<AuthenViewModel, object>(url: $"{url}", req);

            if (res.IsSuccessStatusCode)
            {
                return res.ResponseData;
            }
            return new AuthenViewModel
            {
                Message = res.Message ?? "User is not valid"
            };
        }

        public async Task<BaseResponse<RegisterViewModel>> Register(RegisterModel request)
        {
            string url = "https://localhost:44321/api/account/Register";
            var req = new BaseRequest<RegisterModel>(request);
            var res = await _aPIExcute.PostData<RegisterViewModel, RegisterModel>(url, req);
            return res;
        }

        public async Task<BaseResponse<bool>> CheckEmailExist(string email)
        {
            string url = $"https://localhost:44321/api/account/CheckEmail/{email}";
			HttpResponseMessage response = null;
			response = await httpClient.GetAsync(url);
			string responseData = await response.Content.ReadAsStringAsync();
			var result = new BaseResponse<bool>();
			if (response.IsSuccessStatusCode)
			{
				result = JsonConvert.DeserializeObject<BaseResponse<bool>>(responseData);

			}
			else
			{
				result = JsonConvert.DeserializeObject<BaseResponse<bool>>(responseData);
			}
			return result;
        }

        public async Task<UserProfileViewModel> GetInfo(string userId)
        {
            string url = $"https://localhost:44321/api/account/Profile/{userId}";
            HttpResponseMessage response = null;
            response = await httpClient.GetAsync(url);
            string responseData = await response.Content.ReadAsStringAsync();
            var result = new UserProfileViewModel();
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<UserProfileViewModel>(responseData);

            }
            else
            {
                result = new UserProfileViewModel();
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

using Admin_site.Interface;
using Newtonsoft.Json;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System.Text;

namespace Admin_site.Service
{
    public class AuthenApi : IAuthenApi
    {
        protected APIExcute _aPIExcute;

        public AuthenApi()
		{
            _aPIExcute = new APIExcute();
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

		public async Task<AuthenViewModel> GetUser(string token)
		{
			var url = "https://localhost:44321/api/account/profile";
			var response = await _aPIExcute.GetData<AuthenViewModel>(url, token: token);
			if (response.IsSuccessStatusCode)
			{
				return response.ResponseData;
			}
			return null;
		}

	}
}

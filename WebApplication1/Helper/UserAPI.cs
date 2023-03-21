using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class UserAPI : IUserAPI
	{
        protected APIExcute _aPIExcute;
        public UserAPI()
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
    }
}

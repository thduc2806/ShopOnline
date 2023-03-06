using Identity.ViewModel;
using oShopSolution.Application.Helper;

namespace Identity.Services.Interface
{
    public interface IAuthenticateService
    {
        Task<BaseResponse<AuthenticateViewModel>> AuthenticateByEmail(string email, string password);
    }
}

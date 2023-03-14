using Identity.Database.Entities;
using Identity.ViewModel;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.System.Users;

namespace Identity.Services.Interface
{
    public interface IAccountService
    {
        Task<BaseResponse<bool>> UpdateUser(Users user);
        BaseResponse<RegisterResultModel> Register(RegisterModel model, string creatorId="");

        Task<BaseResponse<AuthenViewModel>> GetUserProfile(string key);
    }
}

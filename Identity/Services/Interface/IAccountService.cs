using Identity.Database.Entities;
using Identity.ViewModel;
using oShopSolution.Application.Helper;

namespace Identity.Services.Interface
{
    public interface IAccountService
    {
        Task<BaseResponse<bool>> UpdateUser(Users user);
        BaseResponse<RegisterResultModel> Register(RegisterModel model);

        //Task<BaseResponse<AuthenViewModel>> GetUserProfile(string key);
    }
}

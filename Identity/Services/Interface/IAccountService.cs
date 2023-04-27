using Identity.Database.Entities;
using Identity.ViewModel;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Common;

namespace Identity.Services.Interface
{
    public interface IAccountService
    {
        Task<BaseResponse<bool>> UpdateUser(Users user);
        BaseResponse<RegisterResultModel> Register(RegisterModel model);

        BaseResponse<bool> CheckEmailExist(string email);

        Task<UserProfileViewModel> GetUserProfile(string userId);

        Task<PageResult<UserProfileViewModel>> GetUser(int page = 1, int pageSize = 10);

        Task<bool> ChangeStatus(string userId);

        //Task<BaseResponse<AuthenViewModel>> GetUserProfile(string key);
    }
}

using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;

namespace Admin_site.Interface
{
    public interface IAccountApi
    {
        Task<PageResult<UserProfileViewModel>> GetUser(PageRequestBase request);

        Task<bool> GetUser(string userId);
    }
}

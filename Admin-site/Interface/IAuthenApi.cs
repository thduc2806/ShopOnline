using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;

namespace Admin_site.Interface
{
    public interface IAuthenApi
    {
		Task<AuthenViewModel> Authenticate(AuthenModel request);

	}
}

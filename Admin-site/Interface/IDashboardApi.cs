using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Dashboard;

namespace Admin_site.Interface
{
	public interface IDashboardApi
	{
        Task<DashboardViewModel> GetTotal();

    }
}

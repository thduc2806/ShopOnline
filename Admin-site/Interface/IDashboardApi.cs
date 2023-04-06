using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Dashboard;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;

namespace Admin_site.Interface
{
	public interface IDashboardApi
	{
        Task<DashboardViewModel> GetTotal();

		Task<PageResult<OrderViewModel>> GetOrder(GetOrderModel request);


	}
}

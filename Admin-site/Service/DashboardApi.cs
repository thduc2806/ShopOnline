using Admin_site.Interface;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Dashboard;

namespace Admin_site.Service
{
	public class DashboardApi : IDashboardApi
	{
		protected APIExcute _aPIExcute;
		public DashboardApi()
		{
			_aPIExcute = new APIExcute();
		}

		public async Task<DashboardViewModel> GetTotal()
		{
			string url = "https://localhost:5001/api/Dashboard";
			var data = await _aPIExcute.GetData<DashboardViewModel>(url);
			return data.ResponseData;
		}
	}
}

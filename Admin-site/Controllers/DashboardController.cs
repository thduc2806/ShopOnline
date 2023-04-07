using Admin_site.Interface;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Catalog.Products;
using System.Security.Claims;

namespace Admin_site.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardApi _dashboardApi;
        public DashboardController(IDashboardApi dashboardApi)
        {
            _dashboardApi = dashboardApi;
        }

		public async Task<IActionResult> Index()
		{
			var result = await _dashboardApi.GetTotal();
			return View(result);
		}

		public async Task<IActionResult> Order(int start = 0, int length = 4, int draw = 0)
        {
			int page = 1;
			int pageSize = length;
			if (start == 0)
			{
				page = 1;
			}
			else
			{
				page = start / length + 1;
			}

			var request = new GetOrderModel()
			{
				PageIndex = page,
				PageSize = pageSize,
			};
			foreach (var item in Request.Query)
			{
				if (item.Key.ToLower() == "search[value]")
					request.Keyword = item.Value;
				if (item.Key.ToLower() == "order[0][column]")
					request.SortBy  = item.Value;
				if (item.Key.ToLower() == "order[0][dir]")
					request.SortDir = item.Value;
			}

			var data = await _dashboardApi.GetOrder(request);
			return Json(new
			{
				draw = draw,
				recordsTotal = data.TotalItems,
				recordsFiltered = data.TotalItems,
				data = data.Items
			});
		}
    }
}

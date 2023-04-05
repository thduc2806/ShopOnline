using Admin_site.Interface;
using Microsoft.AspNetCore.Mvc;
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
    }
}

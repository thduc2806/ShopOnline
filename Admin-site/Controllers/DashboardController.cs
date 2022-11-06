using Microsoft.AspNetCore.Mvc;

namespace Admin_site.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

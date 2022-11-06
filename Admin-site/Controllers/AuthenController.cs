using Microsoft.AspNetCore.Mvc;

namespace Admin_site.Controllers
{
	public class AuthenController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

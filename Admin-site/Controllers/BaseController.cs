using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admin_site.Controllers
{
	public class BaseController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var sessions = context.HttpContext.Session.GetString("Token");
			if (sessions == null)
			{
				context.Result = new RedirectToActionResult("Login", "Authen", null);
			}
			base.OnActionExecuting(context);
		}
	}
}

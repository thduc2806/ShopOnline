using Admin_site.Helper;
using Admin_site.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admin_site.Controllers
{
	public class BaseController : Controller
	{
		private IWorkContext _workContext = null;

		public IWorkContext WorkContext
		{
			get
			{
				if (_workContext == null)
					_workContext = DependencyInjectionHelper.GetService<IWorkContext>();
				return _workContext;

            }
		}

		public BaseController(IWorkContext workContext)
		{
			_workContext = workContext;
		}
        public override void OnActionExecuting(ActionExecutingContext context)
		{
			var sessions = context.HttpContext.Session.GetString("Token");
			if (sessions != null)
			{
				context.Result = new RedirectToActionResult("Login", "Authen", null);
			}
			base.OnActionExecuting(context);
		}
	}
}

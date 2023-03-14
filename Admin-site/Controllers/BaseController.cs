using Admin_site.Helper;
using Admin_site.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admin_site.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
		private IWorkContext _workContext;

		public IWorkContext WorkContext
		{
			get
			{
				if (_workContext == null)
					_workContext = DependencyInjectionHelper.GetService<IWorkContext>();
				return _workContext;

            }
		}

        protected IActionResult Success(object id, string msg = null, string data = null)
        {
            return Ok(new { Id = id, msg = msg, data = data });
        }

        protected IActionResult Error(string message)
        {
            return Ok(new { Id = 0, Msg = message });
        }
    }
}

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
    }
}

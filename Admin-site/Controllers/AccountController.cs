using Admin_site.Interface;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;

namespace Admin_site.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountApi _accountApi;
        public AccountController (IAccountApi accountApi)
        {
            _accountApi = accountApi;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserProfile(int start = 0, int length = 4, int draw = 0)
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

            var request = new PageRequestBase()
            {
                PageIndex = page,
                PageSize = pageSize,
            };

            var data = await _accountApi.GetUser(request);
            return Json(new
            {
                draw = draw,
                recordsTotal = data.TotalItems,
                recordsFiltered = data.TotalItems,
                data = data.Items
            });
        }

        [HttpGet("/account/changestatus/{accountId}")]
        public async Task<IActionResult> ChangeStatus(string accountId)
        {
            var result = await _accountApi.GetUser(accountId);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

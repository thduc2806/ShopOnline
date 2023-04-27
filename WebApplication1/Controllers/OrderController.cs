using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.Catalog.Order;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApi _orderApi;

        public OrderController(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var request = new GetOrderByIdModel()
            {
                PageIndex = pageIndex,
                UserId = userId,
            };
            var order = await _orderApi.GetOrder(request);
            return View(order);
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var result = await _orderApi.GetOrderDetail(orderId);
            return View(result);
        }

    }
}

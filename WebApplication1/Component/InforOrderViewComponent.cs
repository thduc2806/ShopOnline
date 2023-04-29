using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helper;

namespace WebApplication1.Component
{
    public class InforOrderViewComponent : ViewComponent
    {
        private readonly IOrderApi _orderApi;

        public InforOrderViewComponent(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }
        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var result = await _orderApi.GetOrderById(orderId);
            return View("InforOrder", result);
        }
    }
}
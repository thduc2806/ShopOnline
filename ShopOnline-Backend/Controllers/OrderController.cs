using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Order;
using oShopSolution.ViewModels.Catalog.Order;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(InfoCustomerModel request)
		
		{
			var response = await _orderService.CreateOrder(request);
			return Ok(response);
		}

		[HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderModel request)

        {
            var response = await _orderService.UpdatePayment(request);
            return Ok(response);
        }
    }
}

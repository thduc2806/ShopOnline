using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.DashBoard;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTotal(string userId)
        {
            var result = await _dashboardService.GetTotal(userId);
            return Ok(result);
        }
    }
}

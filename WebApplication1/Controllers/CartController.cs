using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.Catalog.Cart;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly ICartApi _cartApi;
        public CartController(IWorkContext workContext, ICartApi cartApi)
        {
            _workContext = workContext;
            _cartApi = cartApi;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

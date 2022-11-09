using Admin_site.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using oShopSolution.ViewModels.Catalog.Products;

namespace Admin_site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApi _productApi;

        public ProductController(IProductApi productApi)
        {
            _productApi = productApi;
        }
        // GET: ProductController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products( int start = 0, int length = 4, int draw = 0)
        {
            int page = 1;
            int pageSize = length;
            if ( start == 0)
            {
                page = 1;
            }
            else
            {
                page = start / length + 1;
            }

            var request = new GetManageProductPageRequest()
            {
                PageIndex = page,
                PageSize = pageSize,
            };

            var data = await _productApi.GetAllProduct(request);
            return Json(new { 
                draw = draw,
                recordsTotal = data.TotalItems,
                recordsFiltered = data.TotalItems,
                data = data.Items });
        }


        // GET: ProductController/Details/5

        public async Task<IActionResult> Details(int Id)
        {
            var result = await _productApi.GetProductById(Id);
            return View(result);
        }


    }
}

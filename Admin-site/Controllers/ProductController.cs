using Admin_site.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 4)
        {
            var request = new GetManageProductPageRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            var data = await _productApi.GetAllProduct(request);
            return View(data);
        }

        // GET: ProductController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        
    }
}

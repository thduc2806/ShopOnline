using Admin_site.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Products;

namespace Admin_site.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductApi _productApi;

        public ProductController(IProductApi productApi, IWorkContext workContext) : base(workContext)
        {
            _productApi = productApi;
           
        }
        // GET: ProductController

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products(int start = 0, int length = 4, int draw = 0)
        {
            var userId = WorkContext.CurrentUser.UserId;
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

            var request = new GetManageProductPageRequest()
            {
                PageIndex = page,
                PageSize = pageSize,
            };
            foreach (var item in Request.Query)
            {
                if (item.Key.ToLower() == "search[value]")
                    request.Keyword = item.Value;
            }

            var data = await _productApi.GetAllProduct(request);
            return Json(new
            {
                draw = draw,
                recordsTotal = data.TotalItems,
                recordsFiltered = data.TotalItems,
                data = data.Items
            });
        }


        // GET: ProductController/Details/5

        public async Task<IActionResult> Details(int Id)
        {
            var result = await _productApi.GetProductById(Id);
            return View(result);
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
			if (!ModelState.IsValid)
				return View(request);
			var product = await _productApi.CreateProduct(request);

			if (product)
			{
				TempData["product"] = "Add success";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", "Add Fail");
			return View(request);
		}

		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{

			var product = await _productApi.GetProductById(Id);
			var editVm = new ProductUpdateRequest()
			{
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
			};
			return View(editVm);
		}

		[HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var product = await _productApi.UpdateProduct(request);

            if(product)
            {
				TempData["product"] = "Update success";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", "Add Fail");
			return View(request);
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productApi.GetProductById(id);
			return View(new ProductView()
            {
                Id = id,
                Name = product?.Name,
                Description = product?.Description,
                Price = product.Price,
                Category = product?.Category,
                CreateDate = product.CreateDate
            });
		}

		[HttpPost]
		public async Task<IActionResult> Delete(ProductDeleteRequest request)
		{
			var result = await _productApi.DeleteProduct(request.Id);
			if (result)
			{
				TempData["result"] = "Delete success";
				return RedirectToAction("Index");
			}

			return View();
		}


	}
}

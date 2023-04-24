using Admin_site.Interface;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.Catalog.Categories;

namespace Admin_site.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryApi _categoryApi;

    public CategoryController(ICategoryApi categoryApi)
    {
        _categoryApi = categoryApi;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var category = await _categoryApi.GetAll();
        return View(category);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        var result = await _categoryApi.Create(request);
        if (result)
        {
            return RedirectToAction("Index","Category");
        }

        return View(request);
    }
}
using oShopSolution.ViewModels.Catalog.Categories;

namespace Admin_site.Interface;

public interface ICategoryApi
{
    Task<List<CategoryView>> GetAll();
    Task<bool> Create(CategoryUpdateRequest request);
}
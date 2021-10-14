using oShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Category
{
	public interface ICategoryService
	{
		Task<List<CategoryView>> GetAll();
		Task<List<CategoryView>> GetById();

	}
}

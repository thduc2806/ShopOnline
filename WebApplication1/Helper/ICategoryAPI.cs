using oShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public interface ICategoryAPI
	{
		//Task<List<CategoryView>> GetAll();

		Task<CategoryView> GetById(int id);
	}
}

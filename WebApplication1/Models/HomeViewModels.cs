using oShopSolution.ViewModels.Catalog.Categories;
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
	public class HomeViewModels
	{
		public PageResult<ProductView> Product { get; set; }
		public List<CategoryView> Category { get; set; }
	}
}

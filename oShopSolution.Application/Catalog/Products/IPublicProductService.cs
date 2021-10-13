
using oShopSolution.ViewModels.Catalog.Products;
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Products
{
	public interface IPublicProductService
	{
		Task<PageResult<ProductView>> GetAllByCategoryId(GetPublicProductPageRequest request);

		Task<List<ProductView>> GetAll();
	}
}

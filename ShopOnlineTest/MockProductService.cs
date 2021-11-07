using Moq;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineTest
{
	public class MockProductService: Mock<IManageProductService>
	{
		public MockProductService MockById(ProductView result, int productId)
		{
			Setup(x => x.GetById(productId))
				.ReturnsAsync(result);
			return this;
		}
		public MockProductService VerifyGetById(Times times, int productId)
		{
			Verify(x => x.GetById(productId), times);

			return this;
		}
	}
}

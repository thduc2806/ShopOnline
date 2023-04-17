using Microsoft.AspNetCore.Mvc;
using Moq;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Products;
using ShopOnline_Backend.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopOnlineTest
{
	public class ProductControllerTest
	{
		[Fact]
		public void ProductController_GetById()
		{
			//Arrange
			var mockProduct = new ProductView()
			{
				Id = 12,
				Name = "Samsung Flip 3",
				Price = 50000,
				Description = "This is samsung Flip 3",
				Rating = 10,
				CategoryId = 2,
				CreateDate = DateTime.Now,
				ThumbImg = "/user-content/ae269ed3-cfdb-4025-959f-191d9152c633.jpg",
				Category = "Samsung",
				//TextComment = null,
				//commentViews = null,
			};
			var mockProductServie = new MockProductService().MockById(mockProduct, 12);
			var controller = new Product1Controller(mockProductServie.Object);

			//Act
			var result = controller.GetById(12);

			//Assert
			Assert.IsAssignableFrom<IActionResult>(result);
			mockProductServie.VerifyGetById(Times.Once(), 12);
		}
	}
}

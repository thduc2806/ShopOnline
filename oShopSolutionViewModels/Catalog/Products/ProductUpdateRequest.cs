using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class ProductUpdateRequest
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please Input Name")]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required(ErrorMessage = "Please Input Price")]
		public decimal Price { get; set; }
		//public string ThumbPath { get; set; }
		//public IFormFile ThumbImg { get; set; }

	}
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class ProductCreateRequest
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public int CategoryId { get; set; }
		public IFormFile ThumbImg { get; set; }
	}
}

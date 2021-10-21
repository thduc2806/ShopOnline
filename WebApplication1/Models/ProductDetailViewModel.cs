using oShopSolution.ViewModels.Catalog.Comments;
using oShopSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
	public class ProductDetailViewModel
	{
		public ProductView Product { get; set; }
		public List<CommentView> Comment { get; set; }
		public CommentRequest CommentRequest { get; set; }
	}
}

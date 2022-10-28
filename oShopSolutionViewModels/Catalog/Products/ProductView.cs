using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class ProductView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public int? Rating { get; set; }
		public int CategoryId { get; set; }
		public DateTime CreateDate { get; set; }
		public string ThumbImg { get; set; }
		public string Category { get; set; }
		//public string TextComment { get; set; }
		//public List<CommentView> commentViews { get; set; }
		//public ICollection<CommentView> ProductComments { get; set; }
		//public List<string> ProductComment { get; set; } = new List<string>();
	}
}

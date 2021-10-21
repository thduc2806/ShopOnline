using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Comments
{
	public class CommentRequest
	{
		public string TextComment { get; set; }
		public int Rating { get; set; }
		public int ProductId { get; set; }
	}
}

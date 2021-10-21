using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Comments
{
	public class CommentView
	{
		public int Id { get; set; }
		public int? ProductId { get; set; }
		public string TextComment { get; set; }
		public int Rating { get; set; }
	}
}

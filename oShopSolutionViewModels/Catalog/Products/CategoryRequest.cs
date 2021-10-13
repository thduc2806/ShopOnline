using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Products
{
	public class CategoryRequest
	{
		public int Id { get; set; }
		public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
	}
}

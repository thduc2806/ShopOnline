
using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
	public class RoleRequest
	{
		public Guid Id { get; set; }
		public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
	public class CreateUserVm
	{
		public string FullName { get; set; }
		public DateTime DOB { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string UseName { get; set; }
		public string Password { get; set; }
	}
}

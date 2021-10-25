using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.ViewModels.Account
{
	public class LoginVm
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}

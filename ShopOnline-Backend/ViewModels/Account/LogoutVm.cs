using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.ViewModels.Account
{
	public class LogoutVm : LogoutInputVm
	{
		public bool ShowLogoutPrompt { get; set; } = true;
	}
}

using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public interface IUserAPI
	{
		Task<AuthenViewModel> Authenticate(AuthenModel request);

	}
}

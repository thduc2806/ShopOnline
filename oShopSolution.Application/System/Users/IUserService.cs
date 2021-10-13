using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.System.Users
{
	public interface IUserService
	{
		Task<string> Authencate(LoginRequest request);
		Task<bool> Register(RegisterRequest request);
	}
}

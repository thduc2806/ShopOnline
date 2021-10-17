using oShopSolution.ViewModels.Common;
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
		Task<ApiResult<string>> Authencate(LoginRequest request);
		Task<ApiResult<bool>> Register(RegisterRequest request);
		Task<ApiResult<bool>> RoleAssign(Guid id, RoleRequest request);
	}
}

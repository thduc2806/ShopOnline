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
		Task<ApiResult<string>> Authenticate(LoginRequest request);
		Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
		Task<ApiResult<bool>> RoleAssign(Guid id, RoleRequest request);
	}
}

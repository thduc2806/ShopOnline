using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.System.Users
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IConfiguration _config;
		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration condfig)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_config = condfig;
		}
		public async Task<ApiResult<string>> Authencate(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.Username);
			if (user == null) return new ApiErrorResult<string>("Account does not exist");

			var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
			if (!result.Succeeded)
			{
				return new ApiErrorResult<string>("Login fail");
			}
			var role = _userManager.GetRolesAsync(user);
			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.FullName),
				new Claim(ClaimTypes.Role, string.Join(";", role))
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Tokens:Issuer"],
				_config["Tokens:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds);
			return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
		}

		public async Task<bool> Register(RegisterRequest request)
		{
			var user = new AppUser()
			{
				DOB = request.DOB,
				FullName = request.FullName,
				UserName = request.Username,
				Email = request.Email,
				PhoneNumber = request.Phone,
			};
			var rs = await _userManager.CreateAsync(user, request.Password);
			if (rs.Succeeded)
			{
				return true;
			}
			else return false;
		}

		public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleRequest request)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return new ApiErrorResult<bool>("Account does not exist");
			}
			var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
			foreach (var roleName in removedRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == true)
				{
					await _userManager.RemoveFromRoleAsync(user, roleName);
				}
			}
			await _userManager.RemoveFromRolesAsync(user, removedRoles);

			var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
			foreach (var roleName in addedRoles)
			{
				if (await _userManager.IsInRoleAsync(user, roleName) == false)
				{
					await _userManager.AddToRoleAsync(user, roleName);
				}
			}

			return new ApiSuccessResult<bool>();
		}
	}
}

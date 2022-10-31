using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
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
		private readonly OShopDbContext _context;
		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration condfig, OShopDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_config = condfig;
			_context = context;
		}
		public async Task<string> Authencate(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.Username);

			if (user == null) 
				return null;

			var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

			if (!result.Succeeded)
			{
				return null;
			}

			var role = _userManager.GetRolesAsync(user);
			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.GivenName, user.FullName),
				new Claim(ClaimTypes.Role, string.Join(";", role))
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
				_config["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


		public async Task<ApiResult<bool>> Register(RegisterRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.Username);
			if (user != null)
			{
				return new ApiErrorResult<bool>("Username already exists");
			}
			if (await _userManager.FindByEmailAsync(request.Email) != null)
			{
				return new ApiErrorResult<bool>("Email already exists");
			}
			user = new AppUser()
			{
				DOB = request.DOB,
				FullName = request.FullName,
				UserName = request.Username,
				Email = request.Email,
				PhoneNumber = request.Phone,
			};
			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				return new ApiSuccessResult<bool>();
			}
			return new ApiErrorResult<bool>("Register Success");
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

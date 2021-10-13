using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
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
		public async Task<string> Authencate(LoginRequest request)
		{
			var user = await _userManager.FindByNameAsync(request.Username);
			if (user == null) return null;
			var rs = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
			if (!rs.Succeeded)
			{
				return null;
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
			return new JwtSecurityTokenHandler().WriteToken(token);
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
	}
}

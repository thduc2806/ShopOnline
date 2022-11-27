using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oShopSolution.Application.System.Users;
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

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthenController(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		[Route("login")]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(request.Username);
				if (user == null)
				{
					return BadRequest(new
					{
						message = "Username or password is incorrect. Please try again"
					}
					 );
				}
				var isCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
				if (!isCorrect)
				{
					return BadRequest(new
					{
						message = "Username or password is incorrect. Please try again"
					}
					 );
				}
				if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
				{
					var userRoles = await _userManager.GetRolesAsync(user);
					var authClaims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, request.Username),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
					};
					foreach (var userRole in userRoles)
					{
						authClaims.Add(new Claim(ClaimTypes.Role, userRole));
					}
					var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var token = new JwtSecurityToken(
						issuer: _configuration["Jwt:Issuer"],
						audience: _configuration["Jwt:Audience"],
						expires: DateTime.Now.AddHours(3),
						claims: authClaims,
						signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
						);
					return Ok(new
					{
						User = user.UserName,
						accessToken = new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token)),
					});
				}
			}
			return Unauthorized();
		}

		//[HttpPost]
		//public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		//{
		//	if (!ModelState.IsValid)
		//		return BadRequest(ModelState);

		//	var result = await _userService.Register(request);
		//	if (!result.IsSuccessed)
		//	{
		//		return BadRequest(result);
		//	}
		//	return Ok(result);
		//}

		//[HttpGet]
		//public IEnumerable<AppUser> GetUser()
		//{
		//	var user = _context.Users.Select(s => new AppUser
		//	{
		//		Id = s.Id,
		//		FullName = s.FullName,
		//		DOB = s.DOB,
		//		PhoneNumber = s.PhoneNumber,
		//		UserName = s.UserName,
		//		Email = s.Email,
		//	}).ToList();
		//	return user;
		//}

		//[HttpDelete("{id}")]
		//public async Task<IActionResult> Delete(Guid id)

		//{
		//	var user = _context.Users.Find(id);
		//	_context.Users.Remove(user);
		//	await _context.SaveChangesAsync();
		//	return Ok();
		//}

		//[HttpPut("{id}")]
		//[Consumes("application/json")]
		//public async Task<IActionResult> Put([FromBody] UserViewModel request, Guid id)
		//{
		//	var user = _context.Users.Find(id);
		//	user.FullName = request.FullName;
		//	//user.DOB = request.DOB;
		//	user.PhoneNumber = request.PhoneNumber;
		//	await _context.SaveChangesAsync();
		//	return Ok(1);
		//}

		//[HttpGet("{id}")]
		//public AppUser Get(Guid id)
		//{
		//	var user = _context.Users.Select(s => new AppUser
		//	{
		//		Id = s.Id,
		//		FullName = s.FullName,
		//		DOB = s.DOB,
		//		PhoneNumber = s.PhoneNumber,
		//		UserName = s.UserName,
		//		Email = s.Email,
		//	}).Where(a => a.Id == id).FirstOrDefault();
		//	return user;
		//}

		//[HttpPost]
		//public async Task<IActionResult> Post([FromBody] CreateUserVm request)
		//{
		//	var user = new AppUser()
		//	{
		//		UserName = request.UseName,
		//		DOB = request.DOB,
		//		FullName = request.FullName,
		//		Email = request.Email,
		//		PhoneNumber = request.PhoneNumber,
		//		Description = request.Description
		//	};
		//	_context.Categories.Add(cate);
		//	await _context.SaveChangesAsync();
		//	if (cate.Id > 0)
		//	{
		//		return Ok();
		//	}
		//	return BadRequest();
		//}
	}
}

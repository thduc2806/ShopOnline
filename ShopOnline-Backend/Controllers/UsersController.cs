using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.System.Users;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly OShopDbContext _context;
		public UsersController(IUserService userService, OShopDbContext context)
		{
			_userService = userService;
			_context = context;
		}

		[HttpPost("authenticate")]

		public async Task<IActionResult> Authenticate([FromBody]LoginRequest request )
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var rsToken = await _userService.Authencate(request);
			if (string.IsNullOrEmpty(rsToken))
			{
				return BadRequest(rsToken);
			}
			return Ok(rsToken);
		}

		[HttpPost]

		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Register(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpGet]
		public IEnumerable<AppUser> GetUser()
		{
			var user = _context.Users.Select(s => new AppUser
			{
				Id = s.Id,
				FullName = s.FullName,
				DOB = s.DOB,
				PhoneNumber = s.PhoneNumber,
				UserName = s.UserName,
				Email = s.Email,
			}).ToList();
			return user;
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)

		{
			var user = _context.Users.Find(id);
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		[Consumes("application/json")]
		public async Task<IActionResult> Put([FromBody] UserViewModel request, Guid id)
		{
			var user = _context.Users.Find(id);
			user.FullName = request.FullName;
			//user.DOB = request.DOB;
			user.PhoneNumber = request.PhoneNumber;
			await _context.SaveChangesAsync();
			return Ok(1);
		}

		[HttpGet("{id}")]
		public AppUser Get(Guid id)
		{
			var user = _context.Users.Select(s => new AppUser
			{
				Id = s.Id,
				FullName = s.FullName,
				DOB = s.DOB,
				PhoneNumber = s.PhoneNumber,
				UserName = s.UserName,
				Email = s.Email,
			}).Where(a => a.Id == id).FirstOrDefault();
			return user;
		}

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

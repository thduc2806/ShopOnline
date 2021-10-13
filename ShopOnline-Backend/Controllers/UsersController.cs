using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.System.Users;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("authenticate")]
		[AllowAnonymous]

		public async Task<IActionResult> Authenticate([FromForm]LoginRequest request )
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var rsToken = await _userService.Authencate(request);
			if (string.IsNullOrEmpty(rsToken))
			{
				return BadRequest("Username or Password is incorrect");
			}
			return Ok(new { token = rsToken });
		}

		[HttpPost("register")]
		[AllowAnonymous]

		public async Task<IActionResult> Authenticate([FromForm] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var rs = await _userService.Register(request);
			if (!rs)
			{
				return BadRequest("Register Faild");
			}
			return Ok();
		}
	}
}

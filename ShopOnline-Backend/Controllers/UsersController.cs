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

		public async Task<IActionResult> Authenticate([FromBody]LoginRequest request )
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var rsToken = await _userService.Authencate(request);
			if (string.IsNullOrEmpty(rsToken.ResultObj))
			{
				return BadRequest(rsToken);
			}
			return Ok(rsToken);
		}

		[HttpPost("register")]

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
	}
}

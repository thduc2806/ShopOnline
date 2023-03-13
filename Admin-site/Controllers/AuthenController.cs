using Admin_site.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.System.Users;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Admin_site.Controllers
{
	public class AuthenController : Controller
	{
		private readonly IAuthenApi _authenApi;
		private readonly IConfiguration _configuration;

		public AuthenController(IAuthenApi authenApi,
			IConfiguration configuration)
		{
			_authenApi = authenApi;
			_configuration = configuration;
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(AuthenModel request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}
			var result = await _authenApi.Authenticate(request);
			if (!result.IsSuccessed)
			{
				ModelState.AddModelError("Erro", result.Message = "Username or Passsword is Wrong! Please input again!!!");
				return View();
			}

			return RedirectToAction("Index", "Product");
		}

		private ClaimsPrincipal ValidateToken(string jwtToken)
		{
			IdentityModelEventSource.ShowPII = true;

			SecurityToken validatedToken;
			TokenValidationParameters validationParameters = new TokenValidationParameters();

			validationParameters.ValidateLifetime = true;

			validationParameters.ValidAudience = _configuration["Jwt:Issuer"];
			validationParameters.ValidIssuer = _configuration["Jwt:Issuer"];
			validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

			return principal;
		}
	}
}

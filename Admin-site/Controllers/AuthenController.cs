﻿using Admin_site.Interface;
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
using Admin_site.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Admin_site.Controllers
{
	public class AuthenController : BaseController
	{
		private readonly IAuthenApi _authenApi;

		public AuthenController(IAuthenApi authenApi)
		{
			_authenApi = authenApi;
		}

        [AllowAnonymous]
		[HttpGet]
        public IActionResult Login()
		{
			var model = new AuthenModel();
            return View(model);
		}

        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AuthenModel request)
		{
			if (ModelState.IsValid)
			{
				var results = await _authenApi.Authenticate(request);
				if (results.UserRole.ToLower() != "admin")
				{
					return View("Login", request);
				}	
				if (results.AccessToken != null)
				{
					var claims = new List<Claim>()
					{
						new Claim(ClaimTypes.Name, results.Email),
						new Claim(ClaimTypes.Rsa, results.AccessToken),
						new Claim(ClaimTypes.NameIdentifier, results.UserId),
						new Claim(ClaimTypes.Role, results.UserRole)
					};

                    var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = request.RememberMe,
                        ExpiresUtc = WebHelper.ConvertUnixTimeToDate(results.Expired)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    //if (!string.IsNullOrWhiteSpace(returnUrl))
                    //    return Redirect(returnUrl);
                    return RedirectToAction("Index", "Dashboard");
                }
				else
				{
                    ViewBag.Error = results.Message;
                    return View("Login", request);
                }
			}
			return View("Login", request);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Dashboard");
		}
	}
}

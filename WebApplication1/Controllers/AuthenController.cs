﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.ViewModels.System.Users;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IUserAPI _userAPI;

        public AuthenController(IUserAPI userAPI)
        {
            _userAPI = userAPI;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new AuthenModel();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AuthenModel request, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var results = await _userAPI.Authenticate(request);

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

                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = results.Message;
                    return View("Login", request);
                }
            }
            return View("Login", request);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _userAPI.CheckEmailExist(model.Email);
            if (!result.IsValidResponse || !result.ResponseData)
            {
                ViewBag.Message = result.Message;
                return View("Login");
            }
            var registerResults = await _userAPI.Register(model);
            if (registerResults.Message == null)
            {
                ViewBag.Message = "Please input field again";
                return View("Login");
            }
            if (!registerResults.IsSuccessStatusCode)
            {
                ViewBag.Message = registerResults.Message;
                return View("Login");
            }    
            var registerResult = registerResults.ResponseData;
            if (registerResult.IsSuccess)
            {
                ViewBag.Message = "Register Successed!!!";
                return View("Login");
            }
            return View("Login");

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

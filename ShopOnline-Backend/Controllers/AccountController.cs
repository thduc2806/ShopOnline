using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using ShopOnline_Backend.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	public class AccountController : Controller
	{
		private IIdentityServerInteractionService _interaction;
		private SignInManager<AppUser> _signInManager;
		private UserManager<AppUser> _userManager;
		//private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEventService _events;
		public AccountController(UserManager<AppUser> userManager,
			IIdentityServerInteractionService interaction,
			/*RoleManager<IdentityRole> roleManager*//*,*/
			SignInManager<AppUser> signInManager,
			IEventService events)
		{
			_interaction = interaction;
			_events = events;
			_signInManager = signInManager;
			//_roleManager = roleManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginVm { ReturnUrl = returnUrl });
		}

		[HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm, string button)
        {
            var context = await _interaction.GetAuthorizationContextAsync(loginVm.ReturnUrl);

            if (button.Equals("cancel"))
            {
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return Redirect(context.RedirectUri);
            }

            var result = await _signInManager.PasswordSignInAsync(loginVm.Username, loginVm.Password, false, false);

            if (!result.Succeeded)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View("Login", loginVm);
            }

            ViewBag.Error = null;

            return Redirect(loginVm.ReturnUrl);
        }



        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterVm { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm, string button)
        {
            if (button.Equals("cancel"))
            {
                return Redirect("~/");
            }

            if (!ModelState.IsValid)
            {
                return View("Register", registerVm);
            }

            var user = new AppUser()
            {
                UserName = registerVm.Username,
                Email = registerVm.Email,
                PhoneNumber = registerVm.Phone.ToString(),
                FullName = registerVm.FullName,
                DOB = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (!result.Succeeded)
            {
                return View("Register", registerVm);
            }

            //await _userManager.AddToRoleAsync(user, "User");

            await _signInManager.SignInAsync(user, false);

            return Redirect(registerVm.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputVm model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
        }

        private async Task<LogoutVm> BuildLogoutViewModelAsync(string logoutId)
		{
			var vm = new LogoutVm { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

			if (User?.Identity.IsAuthenticated != true)
			{
				// if the user is not authenticated, then just show logged out page
				vm.ShowLogoutPrompt = false;
				return vm;
			}

			var context = await _interaction.GetLogoutContextAsync(logoutId);
			if (context?.ShowSignoutPrompt == false)
			{
				// it's safe to automatically sign-out
				vm.ShowLogoutPrompt = false;
				return vm;
			}

			// show the logout prompt. this prevents attacks where the user
			// is automatically signed out by another malicious web page.
			return vm;
		}

        private async Task<LoggedOutVm> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutVm
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}

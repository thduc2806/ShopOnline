using Identity.Services.Interface;
using Identity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody]RegisterModel register)
        {
            register.RoleId = "1";
            var result = _accountService.Register(register);
            return Ok(result);
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var uid = GetUserId();
            var profile = _accountService.GetUserProfile(uid);
            return Ok(profile);
        }

        private string GetUserId()
        {
            return GetClaimValue(User, "UserId");
        }

        private string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            if (principal != null)
            {
                return principal.Identities.FirstOrDefault().FindFirst(claimType).Value;
            }
            return string.Empty;
        }
    }
}

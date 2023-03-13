using Identity.Services.Interface;
using Identity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authservice;
        public AuthController(IAuthenticateService authenticateService)
        {
            _authservice = authenticateService;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(AuthModel model)
        {
            var result = await _authservice.AuthenticateByEmail(model.Email, model.Password);
            var user = HttpContext.User;
            return Ok(result);
        }
    }
}

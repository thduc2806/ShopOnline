using Admin_site.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using oShopSolution.Application.System.Users;
using oShopSolution.ViewModels.System.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Admin_site.Service
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;



        //LoginRequest IWorkContext.CurrentUser => throw new NotImplementedException();

        public WorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

		public string UserId
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }  
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return userId;
            }
        }
    }
}

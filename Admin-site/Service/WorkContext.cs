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
        private LoginRequest _currentUser = null;
        private string _token;
        private string _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        LoginRequest IWorkContext.CurrentUser => GetCurrentUser();


        //LoginRequest IWorkContext.CurrentUser => throw new NotImplementedException();

        public WorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserInfo
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                ClaimsIdentity identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                _userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                return _userId;
            }
        }

        private LoginRequest GetCurrentUser()
        {
            if (_currentUser != null)
                return _currentUser;
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated || _httpContextAccessor.HttpContext.User is WindowsPrincipal)
                return null;
            _currentUser = JwtTokenHelper.GetUserInfo(_httpContextAccessor.HttpContext.User);
            return string.IsNullOrEmpty(_currentUser.UserId) ? null : _currentUser;
        }
    }
}

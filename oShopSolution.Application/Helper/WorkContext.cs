using Microsoft.AspNetCore.Http;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Helper
{
    public class WorkContext : IWorkContext
    {
        private UserInfoModel _cachedUser;
        private string _token;
        public string _userRole;
        public string _userGuid;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string _identityAdminAPIUrl;
        private readonly APIExcute _aPIExcute;

        public WorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _aPIExcute = new APIExcute();
        }

        public string UserToken
        {
            get {
                if (_token != null)
                    return _token;

                if (_httpContextAccessor?.HttpContext.User.Identity?.IsAuthenticated == false)
                    return string.Empty;

                ClaimsIdentity identity = _httpContextAccessor?.HttpContext.User.Identity as ClaimsIdentity;

                var claim = identity?.Claims.FirstOrDefault(c => c.Type == "AccessToken");

                _token = identity.FindFirst(ClaimTypes.Rsa).Value;

                return _token;
            }
        }

        public string UserRole
        {
            get
            {
                if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == false)
                    _userRole = string.Empty;

                ClaimsIdentity identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;

                var claim = identity?.Claims?.FirstOrDefault(c => c.Type == "RoleId");

                _userGuid = claim?.Value ?? string.Empty;

                return _userRole;
            }
        }

        public Guid UserGuid
        {
            get
            {
                if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == false)
                    _userGuid = string.Empty;

                ClaimsIdentity identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;

                var claim = identity?.Claims?.FirstOrDefault(c => c.Type == "UserId");

                _userGuid = claim?.Value ?? string.Empty;

                if (!string.IsNullOrEmpty(_userGuid))
                    return new Guid(_userGuid);
                return new Guid();
            }
        }

        public UserInfoModel CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                {
                    return _cachedUser;
                }
                
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }
                string token = UserToken;

                if (token == null)
                    return null;

                _cachedUser = GetUser();
                return _cachedUser;
            }
        }

        public string UserRoleName => throw new NotImplementedException();

        public int UserRoleId => throw new NotImplementedException();

        public string UserRoleGuidId => throw new NotImplementedException();

        private UserInfoModel GetUser()
        {
            var url = "https://localhost:44321/api/Account/profile";
            var response = _aPIExcute.GetData<UserInfoModel>(url, null, token: UserToken).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.ResponseData;
            }    
            return null;
        }

        public void ClearCachedUserOnSignOut()
        {
            _cachedUser = null;
            _token = null;
        }
    }
}

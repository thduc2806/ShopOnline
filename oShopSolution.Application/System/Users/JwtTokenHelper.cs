using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.UserSecrets;
using oShopSolution.Application.Helper;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.System.Users
{
    public static class JwtTokenHelper
    {
        private static HttpContext Current => IdentityInjectionHelper.GetService<IHttpContextAccessor>().HttpContext;
        //private static JwtTokenBuilder _tokenBuilder;

        public static List<Claim> GetUserClaims(LoginRequest model)
        {
            return new List<Claim>()
            {
                new Claim("Id", model.UserId ?? string.Empty),
                new Claim("Role", model.Role ?? string.Empty),

            };
        }

        public static LoginRequest GetUserInfo(ClaimsPrincipal user)
        {
            return new LoginRequest()
            {
                UserId = GetClaimValue(user, "Id") ?? string.Empty,
                Role = GetClaimValue(user, "Role") ?? string.Empty
            };
        }

        public static string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            ThrowIfPrincipalNull(principal);
            return principal.Identities.FirstOrDefault().FindFirst(claimType)?.Value;
        }

        private static void ThrowIfPrincipalNull(ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new Exception("ClaimsPrincipal is null.");
            ThrowIfIdentityNull(principal.Identities.FirstOrDefault());
        }

        private static void ThrowIfIdentityNull(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
                throw new Exception("ClaimsIdentity is null.");
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebApplication1.Services
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
    public class AuthenViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string UserRole { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenEffectiveDate { get; set; }
        public string TokenEffectiveTimeStick { get; set; }

    }
}

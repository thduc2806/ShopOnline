using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Helper
{
    public interface IWorkContext
    {
        UserInfoModel CurrentUser { get; }
        string UserToken { get; }
        string UserRoleName { get; }
        Guid UserGuid { get; }
        int UserRoleId { get; }
        string UserRoleGuidId { get; }
        void ClearCachedUserOnSignOut();
    }
}

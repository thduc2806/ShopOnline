using oShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.System.Role
{
    public interface IRoleService
    {
        Task<List<RoleView>> GetAll();
    }
}

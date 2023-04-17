using oShopSolution.ViewModels.Catalog.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.DashBoard
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetTotal();

        Task<TotalProductViewModel> GetTotalProduct();

	}
}

using Microsoft.EntityFrameworkCore;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.DashBoard
{
    public class DashboardService : IDashboardService
    {
        private readonly OShopDbContext _context;
        public DashboardService( OShopDbContext context )
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetTotal(string userId)
        {
            var now = DateTime.Now;
            var monthlyRevenue = DateTime.Now.AddDays(-30);
            var order = await _context.Orders.Where(o => o.UserId.ToString() == userId && o.isPayment && o.OrderDate >= monthlyRevenue && o.OrderDate <= now).ToListAsync();
            if(order.Count < 1)
            {
                return new DashboardViewModel();
            }
            var revenuePriceMonth = order.Sum(o => o.Amount);
            var revenueOrderMonth = order.Count();
            var dashboard = new DashboardViewModel()
            {
                TotalPrice = revenuePriceMonth,
                TotalOrder = revenueOrderMonth,
            };
            return dashboard;
        }
    }
}

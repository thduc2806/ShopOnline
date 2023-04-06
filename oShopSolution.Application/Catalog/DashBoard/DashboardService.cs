using Microsoft.Data.SqlClient;
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

        public async Task<DashboardViewModel> GetTotal()
        {
            var now = DateTime.Now;
            var monthlyRevenue = DateTime.Now.AddDays(-30);
            var currentYear = now.Year;
            var order = await _context.Orders.Where(o => o.isPayment && o.OrderDate >= monthlyRevenue && o.OrderDate <= now).ToListAsync();
            if(order.Count < 1)
            {
                return new DashboardViewModel();
            }
            var revenuePriceMonth = order.Sum(o => o.Amount);

            var revenueOrderMonth = order.Count();

            var revenueMonthly = _context.Orders.Where(o => o.OrderDate.Year == currentYear && o.isPayment)
                .GroupBy(o => o.OrderDate.Month).Select(g => new RevenueMonthly
                {
                    X = g.Key,
                    Y = g.Sum(o => o.Amount)
                })
                .ToList();
            
            var dashboard = new DashboardViewModel()
            {
                TotalPrice = revenuePriceMonth,
                TotalOrder = revenueOrderMonth,
                RevenueMonthly = revenueMonthly,
            };
            return dashboard;
        }
    }
}

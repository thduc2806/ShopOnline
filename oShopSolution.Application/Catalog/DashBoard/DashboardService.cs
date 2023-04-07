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

            var allOrder = await _context.Orders.Where(o => o.OrderDate >= monthlyRevenue && o.OrderDate <= now).ToListAsync();

            var countOrder = allOrder.Count();

            var percentOrder = revenueOrderMonth * 100 / countOrder;

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
                PercentOrder = percentOrder,
                AllOrderMonthly = countOrder,
            };
            return dashboard;
        }

        public async Task<TotalProductViewModel> GetTotalProduct()
        {
            var monthlyRevenue = DateTime.Now.AddDays(-30);
            var now = DateTime.Now;
            var query = from o in _context.Orders
                        join od in _context.OrderDetails on o.Id equals od.OrderId into ood
                        from od in ood.DefaultIfEmpty()
                        join p in _context.Products on od.ProductId equals p.Id into odp
                        from p in odp.DefaultIfEmpty()
                        join c in _context.Categories on p.CategoryId equals c.Id into pc
                        from c in pc.DefaultIfEmpty()
                        where o.isPayment == true && o.OrderDate >= monthlyRevenue && o.OrderDate <= now
                        select new { o, od, p , c};

            int total = await query.CountAsync();

            var data = await query.
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.Catalog.Dashboard
{
    public class DashboardViewModel
    {
        public decimal TotalPrice { get; set; }

        public int TotalProduct { get; set; }

        public int TotalOrder { get; set; }

        public float PercentOrder { get; set; }

        public int AllOrderMonthly { get; set; }

        public List<RevenueMonthly> RevenueMonthly { get; set; }

    }

    public class RevenueMonthly
    {
        public int X { get; set; }
        public decimal Y { get; set; }
    }

}

using oShopSolution.ViewModels.Catalog.Order;
using oShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public interface IOrderApi
    {
        Task<PageResult<OrderViewModel>> GetOrder(GetOrderByIdModel request);

        Task<List<OrderDetailViewModel>> GetOrderDetail(int orderId);
    }
}

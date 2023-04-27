using AutoMapper;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Order;

namespace ShopOnline_Backend.Option
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>();
        }
    }
}
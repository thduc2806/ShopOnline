using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;
using System.Security.Claims;
using WebApplication1.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using oShopSolution.ViewModels.Catalog.Cart;

namespace WebApplication1.Component
{
    public class CartInfoViewComponent : ViewComponent
    {
        private readonly ICartApi _cartApi;
        public decimal RangeRate = 23000;

        public CartInfoViewComponent(ICartApi cartApi)
        {
            _cartApi = cartApi;
        }

        public async Task<IViewComponentResult> InvokeAsync(CartModel model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _cartApi.GetCart(userId);
            decimal totalPrice = result.Sum(x => x.Price * x.Quantity);
            ViewBag.TotalPrice = totalPrice;
            ViewBag.TotalPriceUsd = result.Sum(p => p.Quantity * Math.Round(p.Price / RangeRate, 2));
            //ViewBag.Email = model.Email;
            //ViewBag.FullName = model.FullName;
            //ViewBag.Phone = model.PhoneNumber;
            //ViewBag.Address = model.Address;
            //ViewBag.City = model.City;
            //ViewBag.District = model.District;
            //ViewBag.Ward = model.Ward;
            return View("CartInfo", result);
        }
    }
}

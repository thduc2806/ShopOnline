using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Cart;
using oShopSolution.ViewModels.Catalog.Order;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
	[Authorize]
	public class CheckoutController : Controller
	{
		private readonly ICartApi _cartApi;
		private readonly ICheckoutApi _checkoutApi;
		public decimal RangeRate = 23000;
		public CheckoutController(ICartApi cartApi, ICheckoutApi checkoutApi)
		{
			_cartApi = cartApi;
			_checkoutApi = checkoutApi;
		}


		public async Task<IActionResult> Index()
		{
            return View();
        }
		
		public async Task<IActionResult> Payment(int orderId)
		{
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _cartApi.GetCart(userId);
            decimal totalPrice = result.Sum(x => x.Price * x.Quantity);
            ViewBag.TotalPrice = totalPrice;
			ViewBag.OrderId = orderId;
            ViewBag.TotalPriceUsd = result.Sum(p => p.Quantity * Math.Round(p.Price / RangeRate, 2));
            return View(result);
        }

		public async Task<IActionResult> CreateOrder(InfoCustomerModel model)
		{
			model.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
			var result = await _checkoutApi.CreateOder(model);
			return RedirectToAction("Payment", new {orderId = result});

        }

	}
}

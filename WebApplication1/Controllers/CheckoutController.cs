using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using oShopSolution.Application.Helper;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Cart;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ICartApi _cartApi;
		private readonly IConfiguration _configuration;
		private readonly string _clientId;
		private readonly string _secrecKey;
		private readonly string _mode;
		public decimal RangeRate = 23000;
		public CheckoutController(IConfiguration configuration, ICartApi cartApi)
		{
			_cartApi = cartApi;
			_configuration = configuration;
			_clientId = configuration["PayPal:ClientId"];
			_secrecKey = configuration["PayPal:SecretKey"];
			_mode = configuration["PayPal:Mode"];
		}


		public async Task<IActionResult> Index(string userId)
		{
			var result = await _cartApi.GetCart(userId);
			decimal totalPrice = result.Sum(x => x.Price * x.Quantity);
			ViewBag.TotalPrice = totalPrice;
			return View(result);
		}
		//public async Task<IActionResult> PayPayPayment()
		//{
		//	var config = new Dictionary<string, string>
		//	{
		//		{ "clientId", _clientId },
		//		{ "clientSecret", _secrecKey },
		//		{ "mode", _mode }
		//	};

		//	var accessToken = new OAuthTokenCredential(config).GetAccessToken();
		//	var apiContext = new APIContext(accessToken);

		//	var itemList = new ItemList()
		//	{
		//		items = new List<Item>()
		//	};
		//	var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		//	var cart = await _cartApi.GetCart(userId);
		//	var total = Math.Round(cart.Sum(p => p.Quantity * p.Price) / RangeRate, 2);
			
		//	foreach (var item in cart)
		//	{
		//		itemList.items.Add(new Item()
		//		{
		//			name = item.Name,
		//			currency = "USD",
		//			price = Math.Round((item.Price) / RangeRate, 2).ToString(),
		//			quantity = item.Quantity.ToString(),
		//			sku = "sku",
		//			tax = "0",
		//		});
		//	}
		//	var paypalOrderId = DateTime.Now.Ticks;
		//	var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
		//	var payment = new Payment()
		//	{
		//		intent = "sale",
		//		transactions = new List<Transaction>()
		//		{
		//			new Transaction()
		//			{
		//				amount = new Amount()
		//				{
		//					total = total.ToString(),
		//					currency = "USD",
		//				},
		//				item_list = itemList,
		//				description = $"Invoice #{paypalOrderId}",
		//				invoice_number = paypalOrderId.ToString(),
		//			}
		//		},
		//		redirect_urls = new RedirectUrls()
		//		{
		//			cancel_url = $"{hostname}/checkout/paymentfail",
		//			return_url = $"{hostname}/checkout/paymentsuccess",
		//		},
		//		payer = new Payer()
		//		{
		//			payment_method = "paypal",
		//		}
		//	};

		//	var createPayment = payment.Create(apiContext);
		//	var redirectUrl = createPayment.links.FirstOrDefault(l => l.rel.Equals("approval_url"));

		//	if(redirectUrl != null)
		//	{
		//		return Redirect(redirectUrl.href);
		//	}
		//	else
		//	{
		//		return Redirect("/checkout/paymentfail");
		//	}	
			
		//}

	}
}

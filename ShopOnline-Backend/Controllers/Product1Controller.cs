using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Product1Controller : ControllerBase
	{
		private readonly IManageProductService _manageProductService;
		public Product1Controller(IManageProductService manageProductService)
		{
			_manageProductService = manageProductService;
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var product = _manageProductService.GetById(id);
			if (product == null)
				return BadRequest("Can not find");
			return Ok(product);
		}
	}
}

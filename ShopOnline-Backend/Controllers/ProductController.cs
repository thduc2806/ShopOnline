using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductController : ControllerBase
	{
		private readonly IManageProductService _manageProductService;
		public ProductController(IManageProductService manageProductService)
		{
			_manageProductService = manageProductService;
		}
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Get()
		{
			var product = await _manageProductService.GetAll();
			return Ok(product);
		}

		[HttpGet("Page")]
		public async Task<IActionResult> Get([FromQuery] GetPublicProductPageRequest request)
		{
			var product = await _manageProductService.GetAllByCategoryId(request);
			return Ok(product);
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _manageProductService.GetById(id);
			if (product == null)
				return BadRequest("Can not find");
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
		{
			var rs = await _manageProductService.Create(request);
			if (rs == 0)
				return BadRequest();
			var product = await _manageProductService.GetById(rs);
			return CreatedAtAction(nameof(GetById), new { id = rs}, product);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
		{
			var rs = await _manageProductService.Update(request);
			if (rs == 0)
				return BadRequest();
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var rs = await _manageProductService.Delete(id);
			if (rs == 0)
				return BadRequest();
			return Ok();
		}
		

		[HttpPut("price/{id}/{newPrice}")]
		public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
		{
			var rs = await _manageProductService.UpdatePrice(id, newPrice);
			if (rs)
				return Ok();
			return BadRequest();
		}
	}
}

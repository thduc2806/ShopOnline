using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.Data.EF;
using oShopSolution.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IManageProductService _manageProductService;
		private readonly OShopDbContext _context;
		public ProductController(IManageProductService manageProductService, OShopDbContext context)
		{
			_manageProductService = manageProductService;
			_context = context;
		}
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Get()
		{
			var product = await _manageProductService.GetAll();
			return Ok(product);
		}

		[HttpGet("page")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllPagings([FromQuery] GetPublicProductPageRequest request)
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
		//[Consumes("multipart/form-data")]
		public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
		{
			var rs = await _manageProductService.Create(request);
			if (rs == 0)
				return BadRequest();
			var product = await _manageProductService.GetById(rs);
			return CreatedAtAction(nameof(GetById), new { id = rs }, product);
		}

		[HttpPut("{id}")]
		//[Consumes("multipart/form-data")]
		public async Task<IActionResult> Put([FromBody] ProductUpdateRequest request, int id)
		{
			var product = _context.Products.Find(id);
			product.Name = request.Name;
			product.Description = request.Description;
			product.Price = request.Price;
			await _context.SaveChangesAsync();
			return Ok(1);
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

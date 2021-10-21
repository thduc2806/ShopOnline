using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oShopSolution.Application.Catalog.Comment;
using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService _commentService;
		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var comment = await _commentService.GetAll();
			return Ok(comment);
		}
		[HttpGet("page")]
		public async Task<IActionResult> GetAllByProductId([FromQuery] GetCommentPageRequest request)
		{
			var comment = await _commentService.GetAllByProductId(request);
			return Ok(comment);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var comment = await _commentService.GetById(id);
			if (comment == null)
				return BadRequest("Can not find");
			return Ok(comment);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CommentRequest request)
		{
			var rs = await _commentService.Create(request);
			if (rs == 0)
				return BadRequest();
			var comment = await _commentService.GetById(rs);
			return CreatedAtAction(nameof(GetById), new { id = rs}, comment);
		}
	}
}

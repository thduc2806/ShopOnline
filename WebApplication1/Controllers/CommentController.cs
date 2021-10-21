using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;

namespace WebApplication1.Controllers
{
	public class CommentController : Controller
	{
        private readonly ICommentAPI _commentAPI;
        private readonly IConfiguration _configuration;

        public CommentController(ICommentAPI commentAPI,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _commentAPI = commentAPI;
        }
        [HttpGet]
		public IActionResult Create()
		{
			return View();
		}


	}
}

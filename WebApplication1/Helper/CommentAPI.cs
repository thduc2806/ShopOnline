using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using oShopSolution.Utilities.Constants;
using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public class CommentAPI : BaseApiClient, ICommentAPI
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
        public CommentAPI(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

		public async Task<bool> Create(CommentRequest request)
		{
			var sessions = _httpContextAccessor
				.HttpContext
				.Session;
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.Rating.ToString()), "rating");
            requestContent.Add(new StringContent(request.TextComment.ToString()), "TextComment");
            var response = await client.PostAsync($"/api/Comment/", requestContent);
            return response.IsSuccessStatusCode;
        }

		public async Task<List<CommentView>> GetAll()
		{
			var data = await GetListAsync<CommentView>("/api/Comment");
			return data;
		}

		public async Task<List<CommentView>> GetAllByProductId(GetCommentPageRequest request)
		{
			var data = await GetAsync<List<CommentView>>(
				 $"/api/Comment/page?ProductId={request.ProductId}");
			return data;
		}

		public Task<CommentView> GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}

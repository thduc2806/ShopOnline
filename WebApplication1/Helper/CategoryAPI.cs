using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using oShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public class CategoryAPI : BaseApiClient, ICategoryAPI
	{
		public CategoryAPI(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
			: base(httpClientFactory, httpContextAccessor, configuration)
		{
		}
		public async Task<List<CategoryView>> GetAll()
		{
			return await GetListAsync<CategoryView>("/api/category");
		}

		public async Task<CategoryView> GetById(int id)
		{
			return await GetAsync<CategoryView>($"/api/category/{id}");
		}

	}
}

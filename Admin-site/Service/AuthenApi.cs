using Admin_site.Interface;
using Newtonsoft.Json;
using oShopSolution.ViewModels.Common;
using oShopSolution.ViewModels.System.Users;
using System.Text;

namespace Admin_site.Service
{
    public class AuthenApi : IAuthenApi
    {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenApi(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ApiResult<string>> Authenticate(LoginRequest request)
		{
			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var response = await client.PostAsync("/api/user/authenticate/", httpContent);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
		}

	}
}

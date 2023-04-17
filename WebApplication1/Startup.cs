using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation.AspNetCore;
using oShopSolution.ViewModels.System.Users;
using oShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Services;

namespace WebApplication1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<OShopDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("oShopSolutionDb")));
			services.AddControllersWithViews()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>())
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
			services.AddHttpClient();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Authen/Login";
					//options.AccessDeniedPath = "/User/Forbidden/";
				});

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
			});


			services.AddTransient<IProductAPI, ProductAPI>();
			services.AddTransient<ICategoryAPI, CategoryAPI>();
			services.AddTransient<ICommentAPI, CommentAPI>();
			services.AddTransient<IUserAPI, UserAPI>();
			services.AddTransient<IWorkContext, WorkContext>();
			services.AddTransient<ICartApi, CartApi>();
            services.AddTransient<ICheckoutApi, CheckoutApi>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "Product Category",
					pattern: "{category}/{id}", new
					{
						controllers = "product",
						action = "category"
					});

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

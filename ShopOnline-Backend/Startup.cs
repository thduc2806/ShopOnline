using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using oShopSolution.Application.Catalog.Category;
using oShopSolution.Application.Catalog.Comment;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.Application.Common;
using oShopSolution.Application.System.Users;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using ShopOnline_Backend.IdentityServer;
using System;
using System.Collections.Generic;

namespace ShopOnline_Backend
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
			services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<OShopDbContext>()
				.AddDefaultTokenProviders();
			services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/Account/Login";
			});
			services.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
				options.EmitStaticAudienceClaim = true;
			})
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddAspNetIdentity<AppUser>()
				.AddDeveloperSigningCredential();

			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
			services.AddTransient<IManageProductService, ManageProductServie>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<ICommentService, CommentService>();
			services.AddTransient<IStorageService, FileStorageService>();
			services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
			services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
			services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
			services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();


			services.AddControllersWithViews();

			services.AddAuthentication()
				.AddLocalApi("Bearer", option =>
				{
					option.ExpectedScope = "shop.api";
				});


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Shop Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Implicit = new OpenApiOAuthFlow
						{
							TokenUrl = new Uri(Configuration["AuthorityUrl"] + "/connect/authorize"),
							AuthorizationUrl = new Uri(Configuration["AuthorityUrl"] + "/connect/authorize"),
							Scopes = new Dictionary<string, string> { { "shop.api", "Online Shop Api" } }
						},
					},
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						new List<string>{ "shop.api" }
					}
				});
			});

			services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
								  builder =>
								  {
									  builder.WithOrigins("http://localhost:3000")
															  .AllowAnyHeader()
														.AllowAnyMethod();
								  });
			});


			//services.AddRazorPages(option =>
			//{
			//	option.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
			//	{
			//		foreach (var selector in model.Selectors)
			//		{
			//			var attributeRouteModel = selector.AttributeRouteModel;
			//			attributeRouteModel.Order = -1;
			//			attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
			//		}
			//	});
			//});
			//string issuer = Configuration.GetValue<string>("Tokens:Issuer");
			//string signingKey = Configuration.GetValue<string>("Tokens:Key");
			//byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

			//services.AddAuthentication(opt =>
			//{
			//	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			//	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			//})
			//.AddJwtBearer(options =>
			//{
			//	options.RequireHttpsMetadata = false;
			//	options.SaveToken = true;
			//	options.TokenValidationParameters = new TokenValidationParameters()
			//	{
			//		ValidateIssuer = true,
			//		ValidIssuer = issuer,
			//		ValidateAudience = true,
			//		ValidAudience = issuer,
			//		ValidateLifetime = true,
			//		ValidateIssuerSigningKey = true,
			//		ClockSkew = System.TimeSpan.Zero,
			//		IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
			//	};
			//});
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseCors(MyAllowSpecificOrigins);
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();
			app.UseCors();
			app.UseIdentityServer();
			app.UseSwagger();
			app.UseSwaggerUI(
				c =>
				{
					c.OAuthClientId("swagger");
					c.OAuthClientSecret("secret");
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopOnline_Backend v1");
				});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				//endpoints.MapRazorPages();

			});
			
		}
	}
}

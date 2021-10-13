using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.Application.Common;
using oShopSolution.Application.System.Users;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend
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
			services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<OShopDbContext>()
				.AddDefaultTokenProviders();

			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
			services.AddTransient<IManageProductService, ManageProductServie>();
			services.AddTransient<IStorageService, FileStorageService>();
			services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
			services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
			services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
			services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();





			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopOnline_Backend", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				  {
					{
					  new OpenApiSecurityScheme
					  {
						Reference = new OpenApiReference
						  {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						  },
						  Scheme = "oauth2",
						  Name = "Bearer",
						  In = ParameterLocation.Header,
						},
						new List<string>()
					  }
					});
			});

			string issuer = Configuration.GetValue<string>("Tokens:Issuer");
			string signingKey = Configuration.GetValue<string>("Tokens:Key");
			byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidateAudience = true,
					ValidAudience = issuer,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = System.TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopOnline_Backend v1"));
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

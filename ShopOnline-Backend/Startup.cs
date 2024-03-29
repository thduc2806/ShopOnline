using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using oShopSolution.Application.Catalog.Cart;
using oShopSolution.Application.Catalog.Category;
using oShopSolution.Application.Catalog.Comment;
using oShopSolution.Application.Catalog.DashBoard;
using oShopSolution.Application.Catalog.Order;
using oShopSolution.Application.Catalog.Products;
using oShopSolution.Application.Common;
using oShopSolution.Application.System.Users;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.System.Users;
using ShopOnline_Backend.IdentityServer;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ShopOnline_Backend.Option;
using ImageConfigOption = oShopSolution.Application.Option.ImageConfigOption;

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
			services.Configure<ImageConfigOption>(Configuration.GetSection(ImageConfigOption.ImageConfig));
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
			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);
			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
			services.AddTransient<IProductService, ProductServie>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<ICommentService, CommentService>();
			services.AddTransient<IStorageService, FileStorageService>();
			services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
			services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
			services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
			services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
			services.AddTransient<ICartService, CartService>();
			services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IDashboardService, DashboardService>();
            //services.AddTransient<IStringLocalizer, StringLocalizer>();

            services.AddControllersWithViews();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });

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
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
								  builder =>
								  {
									  builder.AllowAnyOrigin()
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

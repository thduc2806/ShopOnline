using Admin_site.Interface;
using Admin_site.Service;
using Identity.Database;
using Identity.Database.Entities;
using Identity.Helper;
using Identity.Services.Implement;
using Identity.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Api", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },Array.Empty<string>()}
    });
});
string str = builder.Configuration.GetConnectionString("IdentityDb");
Console.WriteLine(str);
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(str, options =>
                                                                                            options.EnableRetryOnFailure(
                                                                                                maxRetryCount: 1,
                                                                                                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                                                                                                errorNumbersToAdd: null)));
const string ALLOWED_USERNAME_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
builder.Services.AddIdentity<Users, Roles>(options =>
{
    options.User.AllowedUserNameCharacters = ALLOWED_USERNAME_CHARACTERS;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.Configure<JwtTokenOption>(builder.Configuration.GetSection(JwtTokenOption.JwtTokenOptionImportConfig));
builder.Services.AddStaticHelper();
builder.Services.AddOptions();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public static class ServiceCollectionExtension
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        //********************************************************************//
        services.TryAddScoped<IAccountService, AccountService>();
        services.TryAddScoped<IAuthenticateService, AuthenticateService>();
		//services.TryAddScoped<IRoleService, RoleService>();
	}

    public static void AddStaticHelper(this IServiceCollection services)
    {
        DependencyInjectionHelper.Init(ref services);
    }
}

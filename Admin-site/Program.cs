﻿using Admin_site.Interface;
using Admin_site.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
	options.LoginPath = "/Authen/Login";
	options.Cookie.HttpOnly = false;
	options.Cookie.IsEssential = true;
	options.Cookie.SecurePolicy = CookieSecurePolicy.None;
	options.Cookie.SameSite = SameSiteMode.Lax;
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromDays(60);
	//options.DataProtectionProvider = _provider;
	//options.TicketDataFormat = new TicketDataFormat(_provider.CreateProtector(SecurityConstant.AQSecurityMasterProtector));
	//options.Events.OnValidatePrincipal = PrincipalSecurityStampValidator.ValidatePrincipalAsync;
});
builder.Services.ConfigureApplicationCookie(options =>
{

	//options.Cookie.Domain = GetCookieDomain();
	options.Cookie.HttpOnly = false;
	options.Cookie.SameSite = SameSiteMode.Lax;
	options.Cookie.SecurePolicy = CookieSecurePolicy.None;
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromDays(60);
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductApi, ProductApi>();
builder.Services.AddScoped<IAuthenApi, AuthenApi>();
builder.Services.AddScoped<IWorkContext, WorkContext>();
builder.Services.AddScoped<IDashboardApi, DashboardApi>();
builder.Services.AddScoped<ICategoryApi, CategoryApi>();
builder.Services.AddScoped<IAccountApi, AccountApi>();

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();

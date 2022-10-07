using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Data;
using SinaMerchant.Web.Repositories;
using SinaMerchant.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

// Config DbContext
#region Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

// Add Repos to the DI container
#region IoC
builder.Services.AddScoped(typeof(IRepository<>), typeof(ShopRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(10);
    });
#endregion

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

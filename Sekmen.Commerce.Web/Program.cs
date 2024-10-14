var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
_ = builder.Services
    .AddSingleton(_ => builder.Configuration.Get<AppSettingsModel>()!)
    .AddScoped<IBaseService, BaseService>()
    .AddScoped<ICouponService, CouponService>()
    .AddHttpClient()
    .AddHttpClient<ICouponService, CouponService>();
_ = builder.Services
    .AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
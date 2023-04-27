using Business.Services.Abstract;
using Business.Services.Concrete;
using DataAccess.Dapper.Abstract;
using DataAccess.Dapper.Concrete;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();




//Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .CreateLogger();
//Serilog IOC
builder.Host.UseSerilog(Log.Logger);




builder.Services.AddSingleton<IProductService,ProductManager>();
builder.Services.AddSingleton<IProductDal,DpProductDal>();
builder.Services.AddSingleton<IReviewService, ReviewManager>();
builder.Services.AddSingleton<IReviewDal, DpReviewDal>();
builder.Services.AddSingleton<ICartLineService, CartLineManager>();
builder.Services.AddSingleton<ICartService, CartManager>();


builder.Services.AddSession();

builder.Configuration.AddJsonFile("appsettings.json");
string connectionString = builder.Configuration.GetConnectionString("flyerConnectionString");



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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

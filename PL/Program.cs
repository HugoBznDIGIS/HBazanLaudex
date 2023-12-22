using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = new ServiceCollection();
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json");
var configuration = configurationBuilder.Build();
services.AddSingleton<IConfiguration>(configuration);

services.AddDbContext<HBazanLaudexContext>((sp, o) =>
    o.UseSqlServer(sp.GetRequiredService<IDataSourceProvider>().GetConnectionString()));
services.AddSingleton<IDataSourceProvider, DataSourceProvider>();
var serviceProvider = services.BuildServiceProvider();

string? input = "1";
serviceProvider.GetRequiredService<IDataSourceProvider>().CurrentDataSource = input switch
{
    "1" => DataSource.Principal
};

var dbContext = serviceProvider.GetRequiredService<HBazanLaudexContext>();
Console.WriteLine("Cadena: " + dbContext.Database.GetConnectionString());
Console.WriteLine($"Conecta?: {await dbContext.Database.CanConnectAsync()}");

// Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// CONNECTION
builder.Services.AddDbContext<DL.HBazanLaudexContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HBazanLaudex"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LogIn}/{id?}");

app.Run();
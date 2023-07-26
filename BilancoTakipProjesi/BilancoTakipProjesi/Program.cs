using BilancoTakipProjesi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext s�n�f� i�in Dependency Injection yapal�m. ----------------------

builder.Services.AddDbContext<UygulamaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));

// --------------------------------------------------------------------------

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Panel}/{action=Index}/{id?}");

app.Run();

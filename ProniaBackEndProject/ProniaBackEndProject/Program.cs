using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSession(); 

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBlogService, BlogService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); 

app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

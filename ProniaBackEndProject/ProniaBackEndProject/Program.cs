using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.Services;
using Microsoft.AspNetCore.Identity;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddSession(); 

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 8;
    //bu o demekdir ki Password'un minimum uzunlugu 8 characterden ibaret olsun !
    opt.Password.RequireDigit = true;
    //Reqem olmasi Shertdir !
    opt.Password.RequireLowercase = true;
    //Kicik herf olmasi Shertdir !
    opt.Password.RequireUppercase = true;
    //Boyuk herif olmasi Shertdir !
    opt.Password.RequireNonAlphanumeric = true;
    //Simvol olmasi Shertdir !


    opt.User.RequireUniqueEmail = true;
    //Email unique olmalidir !
    opt.SignIn.RequireConfirmedEmail = true;
    //Email confirmed(yeni tesdiqlenme) ucundur !
    opt.Lockout.MaxFailedAccessAttempts = 3;
    //(LOGIN)Yeniden cehd max 3 defe !
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    //(LOGIN)Bu o demekdir 3 defe sehv etdise 30deq bloka dusecek ve gozlemeli olacaq !
    opt.Lockout.AllowedForNewUsers = true;
    //Yeni qeydiyyatdan kecen userlere qadagalari qoymasin(3 defe yeniden yoxlanis ve s.) !


});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();


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
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

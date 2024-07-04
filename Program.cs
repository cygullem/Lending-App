using Microsoft.EntityFrameworkCore;
using CyMvc.Entities;
using Microsoft.AspNetCore.Identity;
using CyMvc.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CymvcContext>(option =>
    option.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Cymvc;TrustServerCertificate=true;Trusted_Connection=True;")
);

// Configure IdentityContext
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Cymvc;TrustServerCertificate=true;Trusted_Connection=True;")
);

builder.Services.AddIdentity<AppUser, AppRole>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
        // options.Lockout.MaxFailedAccessAttempts = 5;
        // options.Lockout.DefaultLockoutAttempts = TimeSpan.FromSeconds(30);
    }
)
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

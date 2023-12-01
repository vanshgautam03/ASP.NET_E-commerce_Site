using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;


using project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add MySQL
var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));

// enabling sessions
builder.Services.AddSession(Option => {
    Option.IdleTimeout = TimeSpan.FromMinutes(30);
    Option.Cookie.HttpOnly = true;
    Option.Cookie.IsEssential = true;
});

// Adding identity service
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
// Add application services.
builder.Services.AddTransient<DbInitializer>();


var app = builder.Build();


// turn on session
app.UseSession();

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

// Setup authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Brief",
    pattern: "brief",
    defaults: new { controller = "Home", action = "Brief" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

// Seed roles
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
// var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
// await DbInitializer.Initialize(
//     scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>(),
//     scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()
// );

app.Run();

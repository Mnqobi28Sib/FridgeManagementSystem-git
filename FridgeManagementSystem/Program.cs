using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using FridgeManagementSystem.Data;
using FridgeManagementSystem.Interfaces;
using FridgeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Remove Identity setup for user authentication and roles
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddRoles<IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>();

// Remove cookie-based authentication
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/MainSystem/Account/Login";
//         options.LogoutPath = "/MainSystem/Account/Logout";
//     });

// Remove authorization policies
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("TechnicianOnly", policy => policy.RequireRole("Technician"));
//     options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
// });

// Add Scoped services for Fridge and Maintenance visit
builder.Services.AddScoped<IFridgeService, FridgeService>();
builder.Services.AddScoped<IMaintenanceVisitService, MaintenanceVisitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforces strict transport security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware
// app.UseAuthentication();
// app.UseAuthorization();

// Define custom routing
app.UseEndpoints(endpoints =>
{
    // Routes specific to the Fridge Management Subsystem
    endpoints.MapControllerRoute(
        name: "TechnicianDashboard",
        pattern: "Technician/Dashboard",
        defaults: new { controller = "Technician", action = "TechnicianDashboard" });

    endpoints.MapControllerRoute(
        name: "customer_dashboard",
        pattern: "Customer/Dashboard",
        defaults: new { controller = "Customer", action = "CustomerDashboard" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

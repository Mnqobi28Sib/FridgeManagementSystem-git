using FridgeManagementSystem.Data;
using FridgeManagementSystem.Interfaces;
using FridgeManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity services (for authentication)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add Controllers with Views
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

// Define custom routing here
app.UseEndpoints(endpoints =>
{
    // Default route points to the landing page
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=RoleSelection}/{id?}");

    // Technician routes
    endpoints.MapControllerRoute(
        name: "technician_dashboard",
        pattern: "Technician/Dashboard",
        defaults: new { controller = "Technician", action = "TechnicianDashboard" });

    // Customer routes
    endpoints.MapControllerRoute(
        name: "customer_dashboard",
        pattern: "Customer/Dashboard",
        defaults: new { controller = "Customer", action = "CustomerDashboard" });

    // Additional default routes (for Razor Pages if needed)
    endpoints.MapControllerRoute(
        name: "default_fallback",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages(); // Enables Razor Pages


app.Run();

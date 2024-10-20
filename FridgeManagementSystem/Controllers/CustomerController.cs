using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FridgeManagementSystem.Models;
using System;
using FridgeManagementSystem.ViewModels;
using FridgeManagementSystem.Interfaces;


public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IFridgeService _fridgeService;
    private readonly IMaintenanceVisitService _maintenanceVisitService;

    public CustomerController(ApplicationDbContext context, IFridgeService fridgeService, IMaintenanceVisitService maintenanceVisitService)
    {
        _context = context;
        _fridgeService = fridgeService;
        _maintenanceVisitService = maintenanceVisitService;
    }

    public async Task<IActionResult> CustomerDashboard()
    {
        var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(customerIdClaim, out int customerId))
        {
            return RedirectToAction("Error", "Home");
        }

        var customer = await _context.Customers
            .Include(c => c.Fridges) // Include related fridges
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer == null)
        {
            return RedirectToAction("Error", "Home");
        }

        var upcomingVisits = await _context.MaintenanceVisits
            .Where(v => v.CustomerId == customerId && !v.IsCompleted) // Get upcoming visits for the customer
            .ToListAsync();

        var viewModel = new CustomerDashboardViewModel
        {
            CustomerId = customerId,
            Fridges = customer.Fridges, // Populate fridges
            UpcomingVisits = upcomingVisits, // Populate upcoming visits
            Message = TempData["SuccessMessage"]?.ToString() // Optional: message from TempData
        };

        return View(viewModel);
    }


    public IActionResult RedirectToCustomerDashboard(int customerId)
    {
        var viewModel = new CustomerDashboardViewModel
        {
            CustomerId = customerId
        };

        return View("CustomerDashboard", viewModel);
    }

    // Schedule a Maintenance Visit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ScheduleVisit(MaintenanceVisit visit)
    {
        if (ModelState.IsValid)
        {
            // Add the maintenance visit to the database
            _context.MaintenanceVisits.Add(visit);
            await _context.SaveChangesAsync();

            // Success message
            TempData["SuccessMessage"] = "Maintenance visit scheduled successfully!";
            return RedirectToAction("UpcomingVisits");
        }

        // If validation fails, return the view with validation errors
        return View(visit);
    }

    // View all fridges owned by the logged-in customer
    public async Task<IActionResult> MyFridges()
    {
        var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(customerId, out int parsedCustomerId))
        {
            return RedirectToAction("Error", "Home");
        }

        var customer = await _context.Customers
            .Include(c => c.Fridges)
            .FirstOrDefaultAsync(c => c.CustomerId == parsedCustomerId);

        if (customer == null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(customer.Fridges);
    }

    // View upcoming maintenance visits for the customer's fridges
    public async Task<IActionResult> UpcomingVisits()
    {
        var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(customerId, out int parsedCustomerId))
        {
            return RedirectToAction("Error", "Home");
        }

        var customer = await _context.Customers
            .Include(c => c.Fridges)
            .ThenInclude(f => f.MaintenanceVisits)
            .FirstOrDefaultAsync(c => c.CustomerId == parsedCustomerId);

        if (customer == null)
        {
            return RedirectToAction("Error", "Home");
        }

        var visits = customer.Fridges.SelectMany(f => f.MaintenanceVisits)
            .Where(v => !v.IsCompleted)
            .ToList();

        return View(visits);
    }

    // View maintenance history for the customer's fridges
    public async Task<IActionResult> CustomerMaintenanceHistory()
    {
        var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(customerId, out int parsedCustomerId))
        {
            return RedirectToAction("Error", "Home");
        }

        // Retrieve the maintenance history for the customer
        var maintenanceHistory = await _context.MaintenanceVisits
            .Where(m => m.CustomerId == parsedCustomerId) // Assuming you have a CustomerId property
            .ToListAsync();

        return View("CustomerMaintenanceHistory", maintenanceHistory);
    }

    // Render the form to report a fault for a specific fridge
    [HttpGet]
    public async Task<IActionResult> ReportFault(int fridgeId)
    {
        var fridge = await _context.Fridges.FindAsync(fridgeId);

        if (fridge == null)
        {
            return NotFound(); // Handle case where fridge is not found
        }

        var technician = await _context.Technicians.FirstOrDefaultAsync();

        if (technician == null)
        {
            return NotFound(); // Handle case where no technicians are available
        }

        // Create and initialize the FaultReport object with both Fridge and Technician
        var faultReport = new FaultReport
        {
            Fridge = fridge, // Set the Fridge object, not just FridgeId
            Technician = technician, // Set the Technician object, not just TechId
            FridgeId = fridgeId, // FridgeId can still be assigned
            TechId = technician.TechId, // TechId can still be assigned
            ReportedDate = DateTime.Now,
            Description = string.Empty
        };

        return View(faultReport);
    }

    // Handle fault report submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReportFault(FaultReport faultReport)
    {
        if (ModelState.IsValid)
        {
            await _context.FaultReports.AddAsync(faultReport);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Fault report submitted successfully.";
            return RedirectToAction("MyFridges");
        }

        return View(faultReport); // Return the view with validation errors
    }

    // Error action (optional)
    public IActionResult Error()
    {
        return View();
    }
}

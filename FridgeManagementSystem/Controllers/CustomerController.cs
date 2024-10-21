using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FridgeManagementSystem.Models;
using System;
using FridgeManagementSystem.ViewModels;
using FridgeManagementSystem.Interfaces;

namespace FridgeManagementSystem.Controllers
{
    
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

        // Customer Dashboard
        public async Task<IActionResult> CustomerDashboard()
        {
            // Get customer ID from Claims
            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Validate and convert customer ID
            if (!int.TryParse(customerIdClaim, out int customerId))
            {
                return RedirectToAction("Error", "Home");
            }

            // Retrieve customer and related data (fridges)
            var customer = await _context.Customers
                .Include(c => c.Fridges)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Retrieve upcoming visits
            var upcomingVisits = await _context.MaintenanceVisits
                .Where(v => v.CustomerId == customerId && !v.IsCompleted)
                .ToListAsync();

            // Create ViewModel
            var viewModel = new CustomerDashboardViewModel
            {
                CustomerId = customerId,
                Fridges = customer.Fridges,
                UpcomingVisits = upcomingVisits,
                Message = TempData["SuccessMessage"]?.ToString()
            };

            return View(viewModel);
        }

        // Redirect to the customer dashboard with the customer's ID
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
                _context.MaintenanceVisits.Add(visit);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Maintenance visit scheduled successfully!";
                return RedirectToAction("UpcomingVisits");
            }

            return View(visit);
        }

        // View all fridges owned by the logged-in customer
        public async Task<IActionResult> MyFridges()
        {
            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(customerIdClaim, out int parsedCustomerId))
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
            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(customerIdClaim, out int parsedCustomerId))
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
            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(customerIdClaim, out int parsedCustomerId))
            {
                return RedirectToAction("Error", "Home");
            }

            var maintenanceHistory = await _context.MaintenanceVisits
                .Where(m => m.CustomerId == parsedCustomerId)
                .ToListAsync();

            return View("CustomerMaintenanceHistory", maintenanceHistory);
        }

        // Report a fault for a specific fridge (GET)
        [HttpGet]
        public async Task<IActionResult> ReportFault(int fridgeId)
        {
            var fridge = await _context.Fridges.FindAsync(fridgeId);

            if (fridge == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians.FirstOrDefaultAsync();

            if (technician == null)
            {
                return NotFound();
            }

            var faultReport = new FaultReport
            {
                Fridge = fridge,
                Technician = technician,
                FridgeId = fridgeId,
                TechId = technician.TechId,
                ReportedDate = DateTime.Now,
                Description = string.Empty
            };

            return View(faultReport);
        }

        // Handle fault report submission (POST)
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

            return View(faultReport);
        }

        // Error action (optional)
        public IActionResult Error()
        {
            return View();
        }
    }
}

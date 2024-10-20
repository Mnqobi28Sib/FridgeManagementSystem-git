using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FridgeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FridgeManagementSystem.ViewModels;

public class TechnicianController : Controller
{
    private readonly ApplicationDbContext _context;

    public TechnicianController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<int> GetCurrentTechnicianIdAsync()
    {
        var techIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(techIdClaim, out int techId))
        {
            return techId;
        }
        else
        {
            // Handle case where technician ID cannot be retrieved (e.g., redirect to login)
            return 0; // Or throw an exception depending on your logic
        }
    }

    public async Task<IActionResult> TechnicianDashboard(string searchQuery)
    {
        var techId = await GetCurrentTechnicianIdAsync();
        if (techId == 0)
        {
            return NotFound(); // Handle case where technician is not found
        }

        var maintenanceHistory = await _context.MaintenanceVisits
            .Include(v => v.Fridge)
            .Include(v => v.Technician)
            .Where(v => v.IsCompleted && v.TechId == techId)
            .ToListAsync();

        var viewModel = new TechnicianDashboardViewModel
        {
            SearchQuery = searchQuery,
            MaintenanceVisits = maintenanceHistory // Changed to use actual maintenance history
        };

        return View(viewModel);
    }

    public IActionResult RedirectToTechnicianDashboard(int technicianId)
    {
        var viewModel = new TechnicianDashboardViewModel
        {
            TechId = technicianId // Fixed this to use technicianId parameter
        };

        return View(viewModel);
    }

    // Handle visit completion and fault reporting
    public async Task<IActionResult> VisitAndReportFault(int visitId)
    {
        var visit = await _context.MaintenanceVisits
            .Include(v => v.Fridge)
            .Include(v => v.Technician)
            .FirstOrDefaultAsync(v => v.Id == visitId);

        if (visit == null) return NotFound(); // Visit not found

        return View(visit); // Render visit details along with a form for fault reporting
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VisitAndReportFault(int visitId, string visitNotes, string faultDescription)
    {
        var visit = await _context.MaintenanceVisits
            .Include(v => v.Fridge)
            .Include(v => v.Technician)
            .FirstOrDefaultAsync(v => v.Id == visitId);

        if (visit == null) return NotFound(); // Visit not found

        // Mark the visit as completed
        visit.Notes = visitNotes;
        visit.IsCompleted = true;

        // If a fault is reported, create a new FaultReport
        if (!string.IsNullOrEmpty(faultDescription))
        {
            FaultReport faultReport = new FaultReport
            {
                Fridge = visit.Fridge,
                Technician = visit.Technician,
                Description = faultDescription,
                ReportedDate = DateTime.Now,
                IsResolved = false
            };

            _context.FaultReports.Add(faultReport); // Add fault report to the database
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(UpcomingVisits)); // Redirect to the list of upcoming visits
    }

    // View the technician's assigned fridges
    public async Task<IActionResult> TechnicianFridges()
    {
        var techId = await GetCurrentTechnicianIdAsync();

        // Fetch the fridges related to this technician asynchronously
        var technicianFridges = await _context.Fridges
            .Where(f => f.TechId == techId)
            .ToListAsync();

        // Return the view with the list of technician's fridges
        return View(technicianFridges);
    }

    // View the technician's maintenance history
    public async Task<IActionResult> TechnicianMaintenanceHistory()
    {
        var techId = await GetCurrentTechnicianIdAsync();

        // Retrieve the maintenance history for the technician
        var maintenanceHistory = await _context.MaintenanceVisits
            .Include(v => v.Fridge)
            .Include(v => v.Technician)
            .Where(m => m.TechId == techId) // Assuming you have a TechnicianId property
            .ToListAsync();

        return View("TechnicianMaintenanceHistory", maintenanceHistory);
    }

    // View all upcoming maintenance visits for the technician
    public async Task<IActionResult> UpcomingVisits()
    {
        var techId = await GetCurrentTechnicianIdAsync();
        if (techId == 0)
        {
            return NotFound(); // Handle case where the technician is not found
        }

        var visits = await _context.MaintenanceVisits
            .Include(v => v.Fridge)
            .Include(v => v.Technician)
            .Where(v => !v.IsCompleted && v.TechId == techId)
            .ToListAsync();

        return View(visits);
    }

    // Render the form to schedule a new maintenance visit
    public IActionResult ScheduleVisit()
    {
        // Provide fridges and technicians as dropdown lists in the form
        ViewBag.Fridges = new SelectList(_context.Fridges, "Id", "Name");
        ViewBag.Technicians = new SelectList(_context.Technicians, "TechId", "TechName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ScheduleVisit(MaintenanceVisit model)
    {
        if (ModelState.IsValid)
        {
            // Ensure required fields are filled
            if (model.ScheduledDate == default)
            {
                ModelState.AddModelError("ScheduledDate", "Scheduled date is required.");
            }

            // Ensure scheduled date is in the future
            if (model.ScheduledDate <= DateTime.Now)
            {
                ModelState.AddModelError("ScheduledDate", "Scheduled date must be in the future.");
            }

            // Get the current technician ID
            var techId = await GetCurrentTechnicianIdAsync();
            if (techId == 0)
            {
                ModelState.AddModelError("TechnicianId", "Technician ID cannot be retrieved.");
                return View(model);
            }

            // Assign the technician ID to the maintenance visit
            model.TechId = techId;

            _context.MaintenanceVisits.Add(model); // Add the new visit to the database
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UpcomingVisits)); // Redirect to upcoming visits
        }

        // If the model state is invalid, return the view with validation errors
        return View(model);
    }

    // Handle updates to a fault report
    public async Task<IActionResult> UpdateFaultReport(int id, FaultReport faultReport)
    {
        if (id != faultReport.ReportId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            // Return the view with validation errors
            return View(faultReport);
        }

        // Update the fault report in the database
        _context.Update(faultReport);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(TechnicianMaintenanceHistory)); // Redirect to the technician maintenance history view
    }
}

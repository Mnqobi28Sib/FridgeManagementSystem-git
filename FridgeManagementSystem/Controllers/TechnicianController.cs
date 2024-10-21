using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FridgeManagementSystem.Models;
using FridgeManagementSystem.Interfaces;
using FridgeManagementSystem.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FridgeManagementSystem.Controllers
{
    [Authorize(Policy = "TechnicianOnly")]
    public class TechnicianController : Controller
    {
        private readonly IMaintenanceVisitService _maintenanceVisitService;
        private readonly IMaintenanceRecordService _maintenanceRecordService;
        private readonly ApplicationDbContext _context;

        public TechnicianController(
            IMaintenanceVisitService maintenanceVisitService,
            IMaintenanceRecordService maintenanceRecordService,
            ApplicationDbContext context)
        {
            _maintenanceVisitService = maintenanceVisitService;
            _maintenanceRecordService = maintenanceRecordService;
            _context = context;
        }

        private async Task<int> GetCurrentTechnicianIdAsync()
        {
            var techIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(techIdClaim, out int techId))
            {
                return techId;
            }
            return 0; // Handle case where technician ID cannot be retrieved
        }

        public async Task<IActionResult> TechnicianDashboard(string searchQuery)
        {
            var techId = await GetCurrentTechnicianIdAsync();
            if (techId == 0) return NotFound();

            var maintenanceHistory = await _maintenanceVisitService.GetCompletedVisitsByTechnicianIdAsync(techId);

            var viewModel = new TechnicianDashboardViewModel
            {
                SearchQuery = searchQuery,
                MaintenanceVisits = maintenanceHistory
            };

            return View(viewModel);
        }

        public async Task<IActionResult> UpcomingVisits()
        {
            var techId = await GetCurrentTechnicianIdAsync();
            if (techId == 0) return NotFound();

            var visits = await _maintenanceVisitService.GetUpcomingVisitsByTechnicianIdAsync(techId);
            return View(visits);
        }

        public async Task<IActionResult> VisitAndReportFault(int visitId)
        {
            var visit = await _maintenanceVisitService.GetVisitByIdAsync(visitId);
            if (visit == null) return NotFound();

            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VisitAndReportFault(int visitId, string visitNotes, string faultDescription)
        {
            var visit = await _maintenanceVisitService.GetVisitByIdAsync(visitId);
            if (visit == null) return NotFound();

            // Mark the visit as completed
            visit.Notes = visitNotes;
            visit.IsCompleted = true;

            // If a fault is reported, create a new FaultReport
            if (!string.IsNullOrEmpty(faultDescription))
            {
                var technicianId = await GetCurrentTechnicianIdAsync();
                var technician = await _context.Technicians.FindAsync(technicianId);
                var fridge = await _context.Fridges.FindAsync(visit.FridgeId); // Get the fridge associated with the visit

                if (fridge == null || technician == null)
                {
                    // Handle not found
                    return NotFound();
                }

                var faultReport = new FaultReport(fridge, technician)
                {
                    Description = faultDescription,
                    ReportedDate = DateTime.Now,
                    IsResolved = false
                };

                await _maintenanceRecordService.CreateFaultReportAsync(faultReport);
            }

            await _maintenanceVisitService.UpdateVisitAsync(visit);
            return RedirectToAction(nameof(UpcomingVisits));
        }

        public IActionResult CreateFaultReport()
        {
            ViewBag.Fridges = new SelectList(_context.Fridges, "Id", "Name");
            ViewBag.Technicians = new SelectList(_context.Technicians, "TechId", "TechName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaultReport(FaultReport faultReport)
        {
            if (ModelState.IsValid)
            {
                await _maintenanceRecordService.CreateFaultReportAsync(faultReport);
                return RedirectToAction(nameof(TechnicianMaintenanceHistory));
            }
            return View(faultReport);
        }

        public async Task<IActionResult> TechnicianFridges()
        {
            var techId = await GetCurrentTechnicianIdAsync();
            var technicianFridges = await _context.Fridges
                .Where(f => f.TechId == techId)
                .ToListAsync();

            return View(technicianFridges);
        }

        public async Task<IActionResult> TechnicianMaintenanceHistory()
        {
            var techId = await GetCurrentTechnicianIdAsync();
            var maintenanceHistory = await _maintenanceRecordService.GetRecordsByTechnicianIdAsync(techId);
            return View("TechnicianMaintenanceHistory", maintenanceHistory);
        }

        public IActionResult ScheduleVisit()
        {
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
                // Validation logic
                if (model.ScheduledDate == default)
                {
                    ModelState.AddModelError("ScheduledDate", "Scheduled date is required.");
                }

                if (model.ScheduledDate <= DateTime.Now)
                {
                    ModelState.AddModelError("ScheduledDate", "Scheduled date must be in the future.");
                }

                var techId = await GetCurrentTechnicianIdAsync();
                if (techId == 0)
                {
                    ModelState.AddModelError("TechnicianId", "Technician ID cannot be retrieved.");
                    return View(model);
                }

                model.TechId = techId;
                await _maintenanceVisitService.ScheduleVisitAsync(model);
                return RedirectToAction(nameof(UpcomingVisits));
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

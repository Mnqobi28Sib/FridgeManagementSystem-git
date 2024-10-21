using FridgeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MaintenanceVisitService : IMaintenanceVisitService
{
    private readonly ApplicationDbContext _context;

    public MaintenanceVisitService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MaintenanceVisit>> GetCompletedVisitsByTechnicianIdAsync(int technicianId)
    {
        return await _context.MaintenanceVisits
            .Where(v => v.TechId == technicianId && v.IsCompleted)
            .ToListAsync();
    }

    public async Task<List<MaintenanceVisit>> GetUpcomingVisitsByTechnicianIdAsync(int technicianId)
    {
        return await _context.MaintenanceVisits
            .Where(v => v.TechId == technicianId && !v.IsCompleted)
            .ToListAsync();
    }

    public async Task<MaintenanceVisit> GetVisitByIdAsync(int visitId)
    {
        return await _context.MaintenanceVisits.FindAsync(visitId);
    }

    public async Task UpdateVisitAsync(MaintenanceVisit visit)
    {
        _context.MaintenanceVisits.Update(visit);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MaintenanceVisit>> GetUpcomingVisitsAsync()
    {
        return await _context.MaintenanceVisits.Where(v => !v.IsCompleted).ToListAsync();
    }

    public async Task<List<MaintenanceVisit>> GetMaintenanceHistoryAsync()
    {
        return await _context.MaintenanceVisits.Where(v => v.IsCompleted).ToListAsync();
    }

    public async Task ScheduleVisitAsync(MaintenanceVisit visit)
    {
        _context.MaintenanceVisits.Add(visit);
        await _context.SaveChangesAsync();
    }
}

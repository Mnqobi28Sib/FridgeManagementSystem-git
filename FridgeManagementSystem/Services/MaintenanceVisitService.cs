using FridgeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class MaintenanceVisitService : IMaintenanceVisitService
{
    private readonly ApplicationDbContext _context;

    public MaintenanceVisitService(ApplicationDbContext context)
    {
        _context = context;
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

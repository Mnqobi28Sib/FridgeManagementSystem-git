using FridgeManagementSystem.Interfaces;
using FridgeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MaintenanceRecordService : IMaintenanceRecordService
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRecordService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Retrieve all maintenance records for a specific technician
    public async Task<List<MaintenanceRecord>> GetRecordsByTechnicianIdAsync(int technicianId)
    {
        return await _context.MaintenanceRecords
            .Where(record => record.TechId == technicianId)
            .ToListAsync();
    }

    // Create a new fault report
    public async Task CreateFaultReportAsync(FaultReport faultReport)
    {
        _context.FaultReports.Add(faultReport);
        await _context.SaveChangesAsync();
    }

    // Get a fault report by ID
    public async Task<FaultReport> GetFaultReportByIdAsync(int id)
    {
        return await _context.FaultReports.FindAsync(id);
    }

    // Update an existing fault report
    public async Task UpdateFaultReportAsync(FaultReport faultReport)
    {
        _context.FaultReports.Update(faultReport);
        await _context.SaveChangesAsync();
    }

    // Delete a fault report
    public async Task DeleteFaultReportAsync(int id)
    {
        var faultReport = await GetFaultReportByIdAsync(id);
        if (faultReport != null)
        {
            _context.FaultReports.Remove(faultReport);
            await _context.SaveChangesAsync();
        }
    }

    // Get all fault reports
    public async Task<List<FaultReport>> GetAllFaultReportsAsync()
    {
        return await _context.FaultReports.ToListAsync();
    }

    // Get fault reports by fridge ID
    public async Task<IEnumerable<FaultReport>> GetFaultReportsByFridgeIdAsync(int fridgeId)
    {
        return await _context.FaultReports
            .Where(report => report.FridgeId == fridgeId)
            .ToListAsync();
    }

    // Get fault reports by technician ID
    public async Task<IEnumerable<FaultReport>> GetFaultReportsByTechnicianIdAsync(int techId)
    {
        return await _context.FaultReports
            .Where(report => report.TechId == techId)
            .ToListAsync();
    }
}

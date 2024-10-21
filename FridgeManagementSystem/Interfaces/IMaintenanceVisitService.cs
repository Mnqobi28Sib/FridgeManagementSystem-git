using FridgeManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMaintenanceVisitService
{
    // Methods for technician-specific visits
    Task<List<MaintenanceVisit>> GetCompletedVisitsByTechnicianIdAsync(int technId);
    Task<List<MaintenanceVisit>> GetUpcomingVisitsByTechnicianIdAsync(int techId);
    Task<MaintenanceVisit> GetVisitByIdAsync(int scheduledDate);
    Task UpdateVisitAsync(MaintenanceVisit visit);
    Task ScheduleVisitAsync(MaintenanceVisit visit);


    // General methods for upcoming visits and maintenance history
    Task<List<MaintenanceVisit>> GetUpcomingVisitsAsync();
    Task<List<MaintenanceVisit>> GetMaintenanceHistoryAsync();
}

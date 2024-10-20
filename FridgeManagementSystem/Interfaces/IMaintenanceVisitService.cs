using FridgeManagementSystem.Models;
using System.Collections.Generic;

public interface IMaintenanceVisitService
{
    Task<List<MaintenanceVisit>> GetUpcomingVisitsAsync();
    Task<List<MaintenanceVisit>> GetMaintenanceHistoryAsync();
    Task ScheduleVisitAsync(MaintenanceVisit visit);
}

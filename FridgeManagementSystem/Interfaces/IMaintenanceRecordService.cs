// IMaintenanceRecordService.cs
using FridgeManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeManagementSystem.Interfaces
{
    public interface IMaintenanceRecordService
    {
        Task CreateFaultReportAsync(FaultReport faultReport);
        Task<IEnumerable<FaultReport>> GetFaultReportsByFridgeIdAsync(int fridgeId);
        Task<IEnumerable<FaultReport>> GetFaultReportsByTechnicianIdAsync(int techId);
        Task<List<MaintenanceRecord>> GetRecordsByTechnicianIdAsync(int techId);
        Task<FaultReport> GetFaultReportByIdAsync(int reportId);
        Task UpdateFaultReportAsync(FaultReport faultReport);
        Task DeleteFaultReportAsync(int reportId);
        
    }
}

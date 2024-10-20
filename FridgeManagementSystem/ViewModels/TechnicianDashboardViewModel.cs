using FridgeManagementSystem.Models;

namespace FridgeManagementSystem.ViewModels
{
    public class TechnicianDashboardViewModel
    {
        public string TechName { get; set; }
        public int TechId { get; set; }
        public IEnumerable<MaintenanceVisit> MaintenanceVisits { get; set; }
        public string SearchQuery { get; set; }
    }
}

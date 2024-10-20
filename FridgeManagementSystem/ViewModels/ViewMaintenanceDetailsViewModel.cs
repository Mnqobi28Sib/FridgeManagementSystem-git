using FridgeManagementSystem.Models;

namespace FridgeManagementSystem.ViewModels
{
    public class ViewMaintenanceDetailsViewModel
    {
        public MaintenanceVisit MaintenanceVisit { get; set; }
        public List<string> IssuesFound { get; set; }
        public List<string> ActionsTaken { get; set; }
    }

}

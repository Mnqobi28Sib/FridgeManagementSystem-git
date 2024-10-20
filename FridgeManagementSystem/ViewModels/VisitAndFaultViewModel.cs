using FridgeManagementSystem.Models;

namespace FridgeManagementSystem.ViewModels
{
    public class VisitAndFaultViewModel
    {
        public MaintenanceVisit ScheduledDate { get; set; }
        public FaultReport FaultReport { get; set; }
        public Fridge Fridge { get; set; }
    }
}

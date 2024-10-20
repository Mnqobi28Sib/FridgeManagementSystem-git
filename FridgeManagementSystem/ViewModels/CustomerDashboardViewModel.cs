using System.Collections.Generic;
using FridgeManagementSystem.Models;

namespace FridgeManagementSystem.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public int CustomerId { get; set; } // Assuming you want to keep track of the Customer ID
        public IEnumerable<Fridge> Fridges { get; set; } // List of fridges owned by the customer
        public IEnumerable<MaintenanceVisit> UpcomingVisits { get; set; } // List of upcoming maintenance visits
        public string Message { get; set; } // Any message to display, e.g., success or error messages
        public Customer Customer {  get; set; }
    }
}

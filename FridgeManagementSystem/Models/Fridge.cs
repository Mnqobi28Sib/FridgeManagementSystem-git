using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class Fridge
    {
        public int FridgeId { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Model { get; set; } = string.Empty; 

        
        public DateTime PurchaseDate { get; set; }

        public string Location { get; set; }
        public bool IsOperational { get; set; }

        public int? TechId { get; set; }
        public Technician? Technician { get; set; }
        public int CustomerId { get; set; } // Foreign key to Customer
        public Customer Customer { get; set; }

        public virtual ICollection<MaintenanceVisit> MaintenanceVisits { get; set; } = new List<MaintenanceVisit>();
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

        // Optional validation for PurchaseDate
        public bool IsPurchaseDateValid()
        {
            return PurchaseDate > DateTime.MinValue; // Adjust validation logic as needed
        }

      
       
    }



}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class Fridge
    {
        [Key] // Marks this property as the primary key
        public int FridgeId { get; set; }

        [Required(ErrorMessage = "Fridge name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fridge model is required.")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = string.Empty;

        public bool IsOperational { get; set; }

        // Foreign keys
        public int? TechId { get; set; } // Nullable if not assigned yet
        public Technician? Technician { get; set; } // Navigation property
        public int CustomerId { get; set; } // Foreign key to Customer
        public Customer Customer { get; set; } // Navigation property

        // Navigation properties for maintenance
        public virtual ICollection<MaintenanceVisit> MaintenanceVisits { get; set; } = new List<MaintenanceVisit>();
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

        // Optional validation for PurchaseDate
        public bool IsPurchaseDateValid()
        {
            return PurchaseDate > DateTime.MinValue; // Adjust validation logic as needed
        }
    }
}

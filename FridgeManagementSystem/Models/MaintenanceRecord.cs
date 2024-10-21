using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public int RecordId { get; set; }

        // Foreign keys
        public int MaintenanceVisitId { get; set; } // Refers to the associated MaintenanceVisit
        public int FridgeId { get; set; } // Refers to the associated Fridge
        public int TechId { get; set; } // Refers to the Technician who performed the maintenance

        // Date when the maintenance activity was performed
        public DateTime ActivityDate { get; set; }

        // Details of the maintenance activity
        public string Details { get; set; } = string.Empty;

        // Indicates if the maintenance activity is completed
        public bool IsCompleted { get; set; }

        // Navigation properties
        public virtual MaintenanceVisit MaintenanceVisit { get; set; }
        public virtual Fridge Fridge { get; set; }
        public virtual Technician Technician { get; set; }
    }
}

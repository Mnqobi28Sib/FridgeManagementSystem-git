using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class MaintenanceRecord
    {
        
        [Key]
        public int RecordId { get; set; }
        public int FridgeId { get; set; }
        public int TechId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Details { get; set; } // Details of the maintenance activity
        public bool IsCompleted { get; set; }

        // Navigation properties
        public Fridge Fridge { get; set; }
        public Technician Technician { get; set; }
    }
}


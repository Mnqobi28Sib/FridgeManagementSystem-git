using FridgeManagementSystem.Models; 
using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class FaultReport
    {
        [Key] // Explicitly marking this as the primary key
        public int ReportId { get; set; }

        // Foreign key for Fridge
        [Required(ErrorMessage = "Fridge is required.")]
        public int? FridgeId { get; set; }

        // Navigation property
        public required Fridge Fridge { get; set; }

        // Foreign key for Technician
        [Required(ErrorMessage = "Technician is required.")]
        public int? TechId { get; set; }

        // Navigation property
        public required Technician Technician { get; set; }

        // Description with validation
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        public DateTime? ReportedDate { get; set; }
        public bool IsResolved { get; set; }
    }
}

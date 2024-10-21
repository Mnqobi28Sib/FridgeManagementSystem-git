using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class MaintenanceVisit
    {
        [Key] // Primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Fridge is required.")]
        public int FridgeId { get; set; } // Foreign key for Fridge
        public Fridge Fridge { get; set; } // Navigation property for Fridge

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; } // Foreign key for Customer
        public Customer Customer { get; set; } // Navigation property for Customer

        [Required(ErrorMessage = "Technician is required.")]
        public int TechId { get; set; } // Foreign key for Technician
        public Technician Technician { get; set; } // Navigation property for Technician

        [Required(ErrorMessage = "Scheduled date is required.")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; } // Optional notes

        public bool IsCompleted { get; set; }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ScheduledDate < DateTime.Now)
            {
                yield return new ValidationResult(
                    "The scheduled date must be in the future.",
                    new[] { nameof(ScheduledDate) }
                );
            }

        }
    }
}

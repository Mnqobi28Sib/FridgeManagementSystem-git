using FridgeManagementSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FridgeManagementSystem.Models
{
    public class MaintenanceVisit
    {
        public int Id { get; set; } // Primary key

        [Required] // Fridge is required
        public int FridgeId { get; set; }
        [Required]
        public int CustomerId { get; set; } // CustomerId is required
        public Fridge Fridge { get; set; } // Navigation property

        [Required] // Technician is required
        public int TechId { get; set; }
        public Technician Technician { get; set; } // Navigation property

        [Required(ErrorMessage = "Scheduled date is required.")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; } // Optional

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

            // Add other custom validation rules as necessary
        }
    }

}


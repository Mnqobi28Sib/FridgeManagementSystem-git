using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystem.Models
{
    public class FaultReport
    {
        [Key]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Fridge is required.")]
        public int? FridgeId { get; set; }
        public virtual Fridge Fridge { get; set; }

        [Required(ErrorMessage = "Technician is required.")]
        public int? TechId { get; set; }
        public virtual Technician Technician { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        public DateTime ReportedDate { get; set; } = DateTime.Now;

        public bool IsResolved { get; set; } = false;

        // Constructor to enforce Fridge and Technician
        public FaultReport(Fridge fridge, Technician technician)
        {
            Fridge = fridge ?? throw new ArgumentNullException(nameof(fridge));
            Technician = technician ?? throw new ArgumentNullException(nameof(technician));
        }

        // Parameterless constructor for EF
        public FaultReport() { }
    }
}

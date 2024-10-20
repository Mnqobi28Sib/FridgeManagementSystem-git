using FridgeManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

public class Technician
{
    [Key]
    public int TechId { get; set; }

    [Required(ErrorMessage = "Technician name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public required string TechName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public required string Phone { get; set; }
    public ICollection<Fridge> Fridges { get; set; } = new List<Fridge>();
    public virtual ICollection<MaintenanceVisit> MaintenanceVisits { get; set; } = new List<MaintenanceVisit>();
}

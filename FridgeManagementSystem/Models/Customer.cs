namespace FridgeManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required.")]
        public string? ContactNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;

        public virtual ICollection<Fridge> Fridges { get; set; } = new List<Fridge>(); // Initialize as an empty list
    }


}

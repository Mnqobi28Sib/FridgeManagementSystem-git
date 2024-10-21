namespace FridgeManagementSystem.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public ICollection<Fridge> Fridges { get; set; }
    }
}

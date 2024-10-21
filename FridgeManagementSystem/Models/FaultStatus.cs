namespace FridgeManagementSystem.Models
{ 
   public class FaultStatus
     {
       public int FaultStatusID { get; set; }
       public string StatusName { get; set; }
       public ICollection<FaultReport> FaultReports { get; set; }
      }
}

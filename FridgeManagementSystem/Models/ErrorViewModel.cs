using System.Diagnostics;

namespace FridgeManagementSystem.Models // Adjust this namespace based on your project structure
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

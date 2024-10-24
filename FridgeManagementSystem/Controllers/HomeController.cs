using FridgeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FridgeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index method now directs to RoleSelection by default if no role is assigned
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            ViewData["PageTitle"] = "Welcome to ChillTech Innovators Fridge Management System";
            ViewData["PageMessage"] = "Efficiently manage your fridges, schedule maintenance, and troubleshoot any issues.";
            return View();
        }


        

        // Optional: This can be used to provide an About page
        public IActionResult About()
        {
            return View();
        }

  
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

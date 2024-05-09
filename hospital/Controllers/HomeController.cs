using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult PrePatientIndex()
        {
            return View();
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PreDrIndex()
        {
            return View();
        }
        public IActionResult DrLogin()
        {
            return View();
        }
        public IActionResult MangerLogin()
        {
            return View();
        }

        public IActionResult DrReport()
        {
            return View();
        }
        public IActionResult PatientReport()
        {
            return View();
        }
        public IActionResult Book()
        {
            return View();
        }
        public IActionResult AddNewDoctor()
        {
            return View();
        }

        
        

        public IActionResult ContinueAsDoc()
        {
            return View();
        }

        public IActionResult ContinueAsPatient()
        {
            return View();
        }

        public IActionResult ContinueAsManager()
        {
            return View();
        }
        public IActionResult Signout()
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

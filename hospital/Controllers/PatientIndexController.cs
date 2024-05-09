using hospital.connect;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class PatientIndexController : Controller
    {
        private readonly HospitalContext context;

        public PatientIndexController(HospitalContext con)
        {
            this.context = con;
        }

        
        public IActionResult PatientIndex()
        {

            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Treatment()
        {
            return View();
        }

        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult Appointments()
        {
            return View();
        }
        public IActionResult PatientReport()
        {
            return View();
        }

    }
}

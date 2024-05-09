using hospital.connect;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class DoctorIndexController : Controller
    {
        private readonly HospitalContext context;

        public DoctorIndexController(HospitalContext con)
        {
            this.context = con;
        }
        public IActionResult DoctorIndex()
        {
            return View();
        }

        public IActionResult PatientReport()
        {
            return View();

        }
        public IActionResult Appointment()
        {
            return View();

        }

    }
}

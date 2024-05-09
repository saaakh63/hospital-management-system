using hospital.connect;
using hospital.Controllers;
using hospital.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HospitalSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HospitalContext context;

        public DoctorController(HospitalContext con)
        {
            this.context = con;
        }

        public IActionResult PreDrIndex()
        {

            return View();
        }
        [HttpGet]
        public IActionResult LoginAsManager()
        {

            return View();
        }
        [HttpGet]
        public IActionResult LoginAsDoctor()
        {

            return View();
        }
        [HttpPost]
        public IActionResult LoginAsManager(string email, string password)

        {
            if (email == "sarakhalifa@manger.com" && password == "123456789")
            {
                return RedirectToAction("", "ManagerIndex"); 
            }
            return View();
        }
        [HttpPost]
        public IActionResult LoginAsDoctor(string email, string password)
        {
            var account = context.Doctors.SingleOrDefault(account => account.Email == email);
          
            if (account is not null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    HttpContext.Session.SetString("UserId", account.Email.ToString());
                    HttpContext.Session.SetString("UserType", "Doctor");


                    var doctorAppointments = context.Appointments
                        .Where(appointment => appointment.Doctor_ID == account.Doctor_Id)
                        .Include(appointment => appointment.Patient)
                        .Include(appointment => appointment.Department)
                        .ToList();

                    // Pass the retrieved appointments to the DoctorIndex view or another view as needed
                    ViewData["DoctorAppointments"] = doctorAppointments;

                    return RedirectToAction("DoctorIndex", "DoctorIndex");
                }
            }
            return View();
        }

        // Add a new action to display doctor's appointments
        public IActionResult DoctorIndex()
        {
            // Retrieve appointments from ViewData
            var doctorAppointments = ViewData["DoctorAppointments"] as List<Appointment>;

            return View(doctorAppointments);
        }
    }
}

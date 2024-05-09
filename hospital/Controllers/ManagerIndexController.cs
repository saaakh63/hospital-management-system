using hospital.connect;
using hospital.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    public class ManagerIndexController : Controller
    {
        private readonly HospitalContext _context;

        public ManagerIndexController(HospitalContext con)
        {
            this._context = con;
        }
        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Doctor_Id == id);
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult ListDoctors()
        //{
        //    var doctors = _context.Doctors.Include(d => d.Department).ToList();
        //    return View(doctors);
        //}

        public IActionResult Signout()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_context.Departments, "Department_Id", "Department_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorAccount doctorAccount)
        {
            ModelState.Remove("Department");
            ModelState.Remove("Appointments");
           // ModelState.Remove()
           if (ModelState.IsValid)
          {
            

                var found = _context.Doctors.SingleOrDefault(account => account.Email == doctorAccount.Email) is not null;
                if (!found)
                {

                    doctorAccount.Password = BCrypt.Net.BCrypt.HashPassword(doctorAccount.Password);

                    _context.Doctors.Add(doctorAccount);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "ManagerIndex");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                }


           }
            
            ViewData["Departments"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", doctorAccount.Department_Id);

            return View(doctorAccount);
        }
        public IActionResult ListDoctors()
        {
            var doctors = _context.Doctors.Include(d => d.Department).ToList();
            return View(doctors);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Department).SingleOrDefault(d => d.Doctor_Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            // Populate the Departments dropdown
            ViewData["Departments"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", doctor.Department_Id);

            return View(doctor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DoctorAccount doctorAccount)
        {
            if (id != doctorAccount.Doctor_Id)
            {
                return NotFound();
            }

          //  if (ModelState.IsValid)
            {
                try
                {
                    // Update the doctor in the database
                    _context.Update(doctorAccount);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctorAccount.Doctor_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ListDoctors");
            }

            // If ModelState is not valid, repopulate the Departments dropdown
            ViewData["Departments"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", doctorAccount.Department_Id);

            return View(doctorAccount);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Department).SingleOrDefault(d => d.Doctor_Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("ListDoctors");
        }

    }
}
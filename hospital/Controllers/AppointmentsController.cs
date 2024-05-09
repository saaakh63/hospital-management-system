using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hospital.connect;
using hospital.models;

namespace hospital.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalContext _context;

        public AppointmentsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var userMail = HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserType") == "Doctor")
            {
                var hospitalContext = _context.Appointments.Include(a => a.Department).Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.Doctor.Email == userMail).Include(a => a.Department).Include(a => a.Doctor).Include(a => a.Patient);
                return View(await hospitalContext.ToListAsync());
            }
            else
            {
                var hospitalContext = _context.Appointments.Include(a => a.Department).Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.Patient.Email == userMail).Include(a => a.Department).Include(a => a.Doctor).Include(a => a.Patient);
                return View(await hospitalContext.ToListAsync());
            }

        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        
        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Id");
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Name");
            ViewData["Patient_Id"] = new SelectList(_context.Accounts, "Patient_ID", "Email");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Patient_name,Department_Id,Doctor_ID,Patient_Id,Appointment_Date,Phonenumber,Symptoms")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Id", appointment.Department_Id);
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Name", appointment.Doctor_ID);
            ViewData["Patient_Id"] = new SelectList(_context.Accounts, "Patient_ID", "Email", appointment.Patient_Id);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Id", appointment.Department_Id);
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Name", appointment.Doctor_ID);
            ViewData["Patient_Id"] = new SelectList(_context.Accounts, "Patient_ID", "Email", appointment.Patient_Id);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Patient_name,Department_Id,Doctor_ID,Patient_Id,Appointment_Date,Phonenumber,Symptoms")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Id", appointment.Department_Id);
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Name", appointment.Doctor_ID);
            ViewData["Patient_Id"] = new SelectList(_context.Accounts, "Patient_ID", "Email", appointment.Patient_Id);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'HospitalContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

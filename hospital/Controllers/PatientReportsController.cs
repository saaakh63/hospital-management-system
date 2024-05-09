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
    public class PatientReportsController : Controller
    {
        private readonly HospitalContext _context;

        public PatientReportsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: PatientReports
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.PatientReports.Include(p => p.Doctor).Include(p => p.Patient);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: PatientReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientReports == null)
            {
                return NotFound();
            }

            var patientReport = await _context.PatientReports
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientReport_Id == id);
            if (patientReport == null)
            {
                return NotFound();
            }

            return View(patientReport);
        }

        // GET: PatientReports/Create
        public IActionResult Create()
        {
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Contact");
            ViewData["Patient_ID"] = new SelectList(_context.Accounts, "Patient_ID", "Email");
            return View();
        }

        // POST: PatientReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientReport_Id,Report_name,Report_MedicationName,Report_Date,cost,prescription,Patient_ID,Doctor_ID")] PatientReport patientReport)
        {
         //   if (ModelState.IsValid)
            {
                _context.Add(patientReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Contact", patientReport.Doctor_ID);
            ViewData["Patient_ID"] = new SelectList(_context.Accounts, "Patient_ID", "Email", patientReport.Patient_ID);
            return View(patientReport);
        }

        // GET: PatientReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientReports == null)
            {
                return NotFound();
            }

            var patientReport = await _context.PatientReports.FindAsync(id);
            if (patientReport == null)
            {
                return NotFound();
            }
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Contact", patientReport.Doctor_ID);
            ViewData["Patient_ID"] = new SelectList(_context.Accounts, "Patient_ID", "Email", patientReport.Patient_ID);
            return View(patientReport);
        }

        // POST: PatientReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientReport_Id,Report_name,Report_MedicationName,Report_Date,cost,prescription,Patient_ID,Doctor_ID")] PatientReport patientReport)
        {
            if (id != patientReport.PatientReport_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientReportExists(patientReport.PatientReport_Id))
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
            ViewData["Doctor_ID"] = new SelectList(_context.Doctors, "Doctor_Id", "Doctor_Contact", patientReport.Doctor_ID);
            ViewData["Patient_ID"] = new SelectList(_context.Accounts, "Patient_ID", "Email", patientReport.Patient_ID);
            return View(patientReport);
        }

        // GET: PatientReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientReports == null)
            {
                return NotFound();
            }

            var patientReport = await _context.PatientReports
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientReport_Id == id);
            if (patientReport == null)
            {
                return NotFound();
            }

            return View(patientReport);
        }

        // POST: PatientReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientReports == null)
            {
                return Problem("Entity set 'HospitalContext.PatientReports'  is null.");
            }
            var patientReport = await _context.PatientReports.FindAsync(id);
            if (patientReport != null)
            {
                _context.PatientReports.Remove(patientReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientReportExists(int id)
        {
          return (_context.PatientReports?.Any(e => e.PatientReport_Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Models.ClinicModels;
using Clinic.Models.DbModels;

namespace Clinic.Controllers
{
    public class AppliedDiagnosController : Controller
    {
        private readonly ClinicContext _context;

        IClinicRepository<AppliedDiagnos> _db;
        IClinicRepository<Diagnos> _diagdb;

        public AppliedDiagnosController(ClinicContext context)
        {
            _db = new SQLRepository<AppliedDiagnos>(context);
            _diagdb = new SQLRepository<Diagnos>(context);
            _context = context;
        }

        // GET: AppliedDiagnos
        public IActionResult Index()
        {
            return View(_context.AppliedDiagnoses.Include(d => d.Diagnos).Include(a => a.Appointment).ToList());
        }

        // GET: AppliedDiagnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliedDiagnos = await _context.AppliedDiagnoses
                .Include(a => a.Appointment)
                .Include(a => a.Diagnos)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (appliedDiagnos == null)
            {
                return NotFound();
            }

            return View(appliedDiagnos);
        }

        // GET: AppliedDiagnos/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id");
            ViewData["DiagnosId"] = new SelectList(_context.Diagnoses, "Id", "Id");
            return View();
        }

        // POST: AppliedDiagnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppliedDiagnos appliedDiagnos)
        {
            if (ModelState.IsValid)
            {
                _db.Create(appliedDiagnos);
               _db.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appliedDiagnos.AppointmentId);
            ViewData["DiagnosId"] = _context.AppliedDiagnoses.Include(p => p.Diagnos).ToList();
            return View(appliedDiagnos);
        }

        // GET: AppliedDiagnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliedDiagnos = await _context.AppliedDiagnoses.SingleOrDefaultAsync(m => m.Id == id);
            if (appliedDiagnos == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appliedDiagnos.AppointmentId);
            ViewData["DiagnosId"] = new SelectList(_context.Diagnoses, "Id", "Id", appliedDiagnos.DiagnosId);
            return View(appliedDiagnos);
        }

        // POST: AppliedDiagnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,DiagnosId,Id")] AppliedDiagnos appliedDiagnos)
        {
            if (id != appliedDiagnos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appliedDiagnos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppliedDiagnosExists(appliedDiagnos.Id))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appliedDiagnos.AppointmentId);
            ViewData["DiagnosId"] = new SelectList(_context.Diagnoses, "Id", "Id", appliedDiagnos.DiagnosId);
            return View(appliedDiagnos);
        }

        // GET: AppliedDiagnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliedDiagnos = await _context.AppliedDiagnoses
                .Include(a => a.Appointment)
                .Include(a => a.Diagnos)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (appliedDiagnos == null)
            {
                return NotFound();
            }

            return View(appliedDiagnos);
        }

        // POST: AppliedDiagnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appliedDiagnos = await _context.AppliedDiagnoses.SingleOrDefaultAsync(m => m.Id == id);
            _context.AppliedDiagnoses.Remove(appliedDiagnos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppliedDiagnosExists(int id)
        {
            return _context.AppliedDiagnoses.Any(e => e.Id == id);
        }
    }
}

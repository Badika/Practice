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
    public class AppointmentsController : Controller
    {
        IClinicRepository<Appointment> _db;
        IClinicRepository<Patient> _patdb;
        IClinicRepository<Doctor> _docDb;
        ClinicContext context;

        public AppointmentsController(ClinicContext context)
        {
            _db = new SQLRepository<Appointment>(context);
            _patdb = new SQLRepository<Patient>(context);
            _docDb = new SQLRepository<Doctor>(context);
            this.context = context;
        }

        // GET: Appointments
        public IActionResult Index()
        {
            _db.GetAll();

            var appointments = context.Appointments.Include(p => p.Patient).Include(d => d.Doctor).ToList();

            return View(appointments);
        }

        // GET: Appointments
        public IActionResult PatientAppointments(int id)
        {
            return View("Index", GetPatientAppointments(id)); 
        }

        private IEnumerable<Appointment> GetPatientAppointments(int id)
        {
            var appointments = context.Appointments
                .Where(a => a.PatientId == id)
                .Include(p => p.Patient)
                .Include(d => d.Doctor)
                .ToList();

            return appointments;
        }

        // GET: Appointments
        public IActionResult DoctorAppointments(int id)
        {
            return View("Index", GetDoctorAppointments(id)); 
        }

        private IEnumerable<Appointment> GetDoctorAppointments(int id)
        {
            var appointments = context.Appointments
                 .Where(a => a.DoctorId == id)
                 .Include(p => p.Patient)
                 .Include(d => d.Doctor)
                 .ToList();

            return appointments;
        }

        // GET: Appointments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = context.Appointments
                .Where(a => a.Id == id)
                .Include(p => p.Patient)
                .Include(d => d.Doctor)
                .FirstOrDefault();

            if (appointment == null)
            {
                return NotFound();
            }

    //        appointment.Patient = _patientDb.GetOne(appointment.PatientId);
         

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Create(appointment);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _db.GetOne((int)id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(appointment);
                    _db.Save();
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
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var appointment = _db.GetOne((int)id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _db.GetAll().Any(e => e.Id == id);
        }
    }
}

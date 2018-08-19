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
        //IClinicRepository<Patient> _patientDb;
        //IClinicRepository<Doctor> _doctorDb;
        ClinicContext _context;

        public AppointmentsController(ClinicContext context)
        {
            _db = new SQLRepository<Appointment>(context);
            //_patientDb = new SQLRepository<Patient>(context);
            //_doctorDb = new SQLRepository<Doctor>(context);
            _context = context;
        }

        // GET: Appointments
        public IActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Appointments
        public IActionResult PatientAppointments(int id)
        {
            return View("Index", GetPatientAppointments(id)); 
        }

        private IEnumerable<Appointment> GetPatientAppointments(int id)
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            //var app = _context.Appointments.Where(i => i.PatientId == id).Include(i => i.Patient).ToList();

            var app = _db.GetAll();
            List<Appointment> list = new List<Appointment>();
            foreach(var i in app)
            {
                if(i.PatientId == id)
                {
                    list.Add(i);
                }
            }

            return list;
        }

        // GET: Appointments
        public IActionResult DoctorAppointments(int id)
        {
            return View("Index", GetDoctorAppointments(id)); ///////////////////
        }

        private IEnumerable<Appointment> GetDoctorAppointments(int id)
        {
            var blog2 = _context.Appointments
                    .Where(b => b.DoctorId == id)
                    .ToList();

            return blog2;
        }

        // GET: Appointments/Details/5
        public IActionResult Details(int? id)
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

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
    public class PatientsController : Controller
    {
        IClinicRepository<Patient> _db;

        public PatientsController(ClinicContext context)
        {
            _db = new SQLRepository<Patient>(context);
        }


        // GET: Patients
        public IActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Patients/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _db.GetOne((int)id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }


        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient p)
        {
            if (ModelState.IsValid)
            {
                _db.Create(p);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(p);
        }


        // GET: Patients/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _db.GetOne((int)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(patient);
                    _db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }


        // GET: Patients/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _db.GetOne((int)id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }


        private bool PatientExists(int id)
        {
            return _db.GetAll().Any(e => e.Id == id);
        }
    }
}

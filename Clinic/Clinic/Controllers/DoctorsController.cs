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
    public class DoctorsController : Controller
    {
        IClinicRepository<Doctor> _db;

        public DoctorsController(ClinicContext context)
        {
            _db = new SQLRepository<Doctor>(context);
        }


        // GET: Doctors
        public IActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Doctors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _db.GetOne((int)id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }


        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor d)
        {
            if (ModelState.IsValid)
            {
                _db.Create(d);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(d);
        }


        // GET: Doctors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _db.GetOne((int)id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(doctor);
                    _db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }


        // GET: Doctors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _db.GetOne((int)id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
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


        private bool DoctorExists(int id)
        {
            return _db.GetAll().Any(e => e.Id == id);
        }
    }
}

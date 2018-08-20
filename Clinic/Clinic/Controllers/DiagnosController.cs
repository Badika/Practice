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
    public class DiagnosController : Controller
    {
        //private readonly ClinicContext _context;
        IClinicRepository<Diagnos> _db;

        public DiagnosController(ClinicContext context)
        {
            _db = new SQLRepository<Diagnos>(context);
        }

        // GET: Diagnos
        public IActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Diagnos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnos = _db.GetOne((int)id);
            if (diagnos == null)
            {
                return NotFound();
            }

            return View(diagnos);
        }

        // GET: Diagnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Diagnos diagnos)
        {
            if (ModelState.IsValid)
            {
                _db.Create(diagnos);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnos);
        }

        // GET: Diagnos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnos = _db.GetOne((int)id);
            if (diagnos == null)
            {
                return NotFound();
            }
            return View(diagnos);
        }

        // POST: Diagnos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Diagnos diagnos)
        {
            if (id != diagnos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(diagnos);
                    _db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosExists(diagnos.Id))
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
            return View(diagnos);
        }

        // GET: Diagnos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnos = _db.GetOne((int)id);
            if (diagnos == null)
            {
                return NotFound();
            }

            return View(diagnos);
        }

        // POST: Diagnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosExists(int id)
        {
            return _db.GetAll().Any(e => e.Id == id);
        }
    }
}

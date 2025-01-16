using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center_Management.Models;

namespace Fitness_Center_Management.Controllers
{
    public class MaininformationoffitnesscentersController : Controller
    {
        private readonly ModelContext _context;

        public MaininformationoffitnesscentersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Maininformationoffitnesscenters
        public async Task<IActionResult> Index()
        {
              return _context.Maininformationoffitnesscenters != null ? 
                          View(await _context.Maininformationoffitnesscenters.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Maininformationoffitnesscenters'  is null.");
        }

        // GET: Maininformationoffitnesscenters/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Maininformationoffitnesscenters == null)
            {
                return NotFound();
            }

            var maininformationoffitnesscenter = await _context.Maininformationoffitnesscenters
                .FirstOrDefaultAsync(m => m.Maininformationoffitnesscenterid == id);
            if (maininformationoffitnesscenter == null)
            {
                return NotFound();
            }

            return View(maininformationoffitnesscenter);
        }

        // GET: Maininformationoffitnesscenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maininformationoffitnesscenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maininformationoffitnesscenterid,Name,Firstabouttext,Secoundabouttext,Thirdabouttext,Openday,Closedday,Worktime,Testamoinaltext,Welcomelocationtext,Locationtext,Locationsource,Copyrighttext,Email,Phone")] Maininformationoffitnesscenter maininformationoffitnesscenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maininformationoffitnesscenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maininformationoffitnesscenter);
        }

        // GET: Maininformationoffitnesscenters/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Maininformationoffitnesscenters == null)
            {
                return NotFound();
            }

            var maininformationoffitnesscenter = await _context.Maininformationoffitnesscenters.FindAsync(id);
            if (maininformationoffitnesscenter == null)
            {
                return NotFound();
            }
            return View(maininformationoffitnesscenter);
        }

        // POST: Maininformationoffitnesscenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Maininformationoffitnesscenterid,Name,Firstabouttext,Secoundabouttext,Thirdabouttext,Openday,Closedday,Worktime,Testamoinaltext,Welcomelocationtext,Locationtext,Locationsource,Copyrighttext,Email,Phone")] Maininformationoffitnesscenter maininformationoffitnesscenter)
        {
            if (id != maininformationoffitnesscenter.Maininformationoffitnesscenterid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maininformationoffitnesscenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaininformationoffitnesscenterExists(maininformationoffitnesscenter.Maininformationoffitnesscenterid))
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
            return View(maininformationoffitnesscenter);
        }

        // GET: Maininformationoffitnesscenters/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Maininformationoffitnesscenters == null)
            {
                return NotFound();
            }

            var maininformationoffitnesscenter = await _context.Maininformationoffitnesscenters
                .FirstOrDefaultAsync(m => m.Maininformationoffitnesscenterid == id);
            if (maininformationoffitnesscenter == null)
            {
                return NotFound();
            }

            return View(maininformationoffitnesscenter);
        }

        // POST: Maininformationoffitnesscenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Maininformationoffitnesscenters == null)
            {
                return Problem("Entity set 'ModelContext.Maininformationoffitnesscenters'  is null.");
            }
            var maininformationoffitnesscenter = await _context.Maininformationoffitnesscenters.FindAsync(id);
            if (maininformationoffitnesscenter != null)
            {
                _context.Maininformationoffitnesscenters.Remove(maininformationoffitnesscenter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaininformationoffitnesscenterExists(decimal id)
        {
          return (_context.Maininformationoffitnesscenters?.Any(e => e.Maininformationoffitnesscenterid == id)).GetValueOrDefault();
        }
    }
}

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
    public class FeaturessectionsController : Controller
    {
        private readonly ModelContext _context;

        public FeaturessectionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Featuressections
        public async Task<IActionResult> Index()
        {
              return _context.Featuressections != null ? 
                          View(await _context.Featuressections.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Featuressections'  is null.");
        }

        // GET: Featuressections/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Featuressections == null)
            {
                return NotFound();
            }

            var featuressection = await _context.Featuressections
                .FirstOrDefaultAsync(m => m.Featuressectionid == id);
            if (featuressection == null)
            {
                return NotFound();
            }

            return View(featuressection);
        }

        // GET: Featuressections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Featuressections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Featuressectionid,Name,Firstlefttext,Secoundlefttext,Thirdlefttext,Button")] Featuressection featuressection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featuressection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(featuressection);
        }

        // GET: Featuressections/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Featuressections == null)
            {
                return NotFound();
            }

            var featuressection = await _context.Featuressections.FindAsync(id);
            if (featuressection == null)
            {
                return NotFound();
            }
            return View(featuressection);
        }

        // POST: Featuressections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Featuressectionid,Name,Firstlefttext,Secoundlefttext,Thirdlefttext,Button")] Featuressection featuressection)
        {
            if (id != featuressection.Featuressectionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featuressection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturessectionExists(featuressection.Featuressectionid))
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
            return View(featuressection);
        }

        // GET: Featuressections/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Featuressections == null)
            {
                return NotFound();
            }

            var featuressection = await _context.Featuressections
                .FirstOrDefaultAsync(m => m.Featuressectionid == id);
            if (featuressection == null)
            {
                return NotFound();
            }

            return View(featuressection);
        }

        // POST: Featuressections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Featuressections == null)
            {
                return Problem("Entity set 'ModelContext.Featuressections'  is null.");
            }
            var featuressection = await _context.Featuressections.FindAsync(id);
            if (featuressection != null)
            {
                _context.Featuressections.Remove(featuressection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturessectionExists(decimal id)
        {
          return (_context.Featuressections?.Any(e => e.Featuressectionid == id)).GetValueOrDefault();
        }
    }
}

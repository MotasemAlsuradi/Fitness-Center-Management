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
    public class TrainerclassesController : Controller
    {
        private readonly ModelContext _context;

        public TrainerclassesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Trainerclasses
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Trainerclasses.Include(t => t.Classtype).Include(t => t.Trainers);
            return View(await modelContext.ToListAsync());
        }

        // GET: Trainerclasses/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Trainerclasses == null)
            {
                return NotFound();
            }

            var trainerclass = await _context.Trainerclasses
                .Include(t => t.Classtype)
                .Include(t => t.Trainers)
                .FirstOrDefaultAsync(m => m.Trainerclassesid == id);
            if (trainerclass == null)
            {
                return NotFound();
            }

            return View(trainerclass);
        }

        // GET: Trainerclasses/Create
        public IActionResult Create()
        {
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid");
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid");
            return View();
        }

        // POST: Trainerclasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Trainerclassesid,Trainersid,Classtypeid")] Trainerclass trainerclass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerclass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", trainerclass.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", trainerclass.Trainersid);
            return View(trainerclass);
        }

        // GET: Trainerclasses/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Trainerclasses == null)
            {
                return NotFound();
            }

            var trainerclass = await _context.Trainerclasses.FindAsync(id);
            if (trainerclass == null)
            {
                return NotFound();
            }
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", trainerclass.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", trainerclass.Trainersid);
            return View(trainerclass);
        }

        // POST: Trainerclasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Trainerclassesid,Trainersid,Classtypeid")] Trainerclass trainerclass)
        {
            if (id != trainerclass.Trainerclassesid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerclass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerclassExists(trainerclass.Trainerclassesid))
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
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", trainerclass.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", trainerclass.Trainersid);
            return View(trainerclass);
        }

        // GET: Trainerclasses/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Trainerclasses == null)
            {
                return NotFound();
            }

            var trainerclass = await _context.Trainerclasses
                .Include(t => t.Classtype)
                .Include(t => t.Trainers)
                .FirstOrDefaultAsync(m => m.Trainerclassesid == id);
            if (trainerclass == null)
            {
                return NotFound();
            }

            return View(trainerclass);
        }

        // POST: Trainerclasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Trainerclasses == null)
            {
                return Problem("Entity set 'ModelContext.Trainerclasses'  is null.");
            }
            var trainerclass = await _context.Trainerclasses.FindAsync(id);
            if (trainerclass != null)
            {
                _context.Trainerclasses.Remove(trainerclass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerclassExists(decimal id)
        {
          return (_context.Trainerclasses?.Any(e => e.Trainerclassesid == id)).GetValueOrDefault();
        }
    }
}

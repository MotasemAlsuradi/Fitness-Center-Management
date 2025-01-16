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
    public class SchedulesController : Controller
    {
        private readonly ModelContext _context;

        public SchedulesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Schedules.Include(s => s.Classtype).Include(s => s.Trainers);
            return View(await modelContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Classtype)
                .Include(s => s.Trainers)
                .FirstOrDefaultAsync(m => m.Scheduleid == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid");
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Scheduleid,Classtypeid,Trainersid,Isactive")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", schedule.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", schedule.Trainersid);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", schedule.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", schedule.Trainersid);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Scheduleid,Classtypeid,Trainersid,Isactive")] Schedule schedule)
        {
            if (id != schedule.Scheduleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Scheduleid))
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
            ViewData["Classtypeid"] = new SelectList(_context.Classtypes, "Classtypeid", "Classtypeid", schedule.Classtypeid);
            ViewData["Trainersid"] = new SelectList(_context.Trainers, "Trainersid", "Trainersid", schedule.Trainersid);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Classtype)
                .Include(s => s.Trainers)
                .FirstOrDefaultAsync(m => m.Scheduleid == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'ModelContext.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(decimal id)
        {
          return (_context.Schedules?.Any(e => e.Scheduleid == id)).GetValueOrDefault();
        }
    }
}

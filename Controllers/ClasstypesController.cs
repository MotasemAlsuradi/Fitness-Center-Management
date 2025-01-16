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
    public class ClasstypesController : Controller
    {
        private readonly ModelContext _context;

        public ClasstypesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Classtypes
        public async Task<IActionResult> Index()
        {
              return _context.Classtypes != null ? 
                          View(await _context.Classtypes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Classtypes'  is null.");
        }

        // GET: Classtypes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Classtypes == null)
            {
                return NotFound();
            }

            var classtype = await _context.Classtypes
                .FirstOrDefaultAsync(m => m.Classtypeid == id);
            if (classtype == null)
            {
                return NotFound();
            }

            return View(classtype);
        }

        // GET: Classtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Classtypeid,Classname,Dayofweek,Starttime,Endtime,Classdescription")] Classtype classtype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classtype);
        }

        // GET: Classtypes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Classtypes == null)
            {
                return NotFound();
            }

            var classtype = await _context.Classtypes.FindAsync(id);
            if (classtype == null)
            {
                return NotFound();
            }
            return View(classtype);
        }

        // POST: Classtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Classtypeid,Classname,Dayofweek,Starttime,Endtime,Classdescription")] Classtype classtype)
        {
            if (id != classtype.Classtypeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasstypeExists(classtype.Classtypeid))
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
            return View(classtype);
        }

        // GET: Classtypes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Classtypes == null)
            {
                return NotFound();
            }

            var classtype = await _context.Classtypes
                .FirstOrDefaultAsync(m => m.Classtypeid == id);
            if (classtype == null)
            {
                return NotFound();
            }

            return View(classtype);
        }

        // POST: Classtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Classtypes == null)
            {
                return Problem("Entity set 'ModelContext.Classtypes'  is null.");
            }
            var classtype = await _context.Classtypes.FindAsync(id);
            if (classtype != null)
            {
                _context.Classtypes.Remove(classtype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasstypeExists(decimal id)
        {
          return (_context.Classtypes?.Any(e => e.Classtypeid == id)).GetValueOrDefault();
        }
    }
}

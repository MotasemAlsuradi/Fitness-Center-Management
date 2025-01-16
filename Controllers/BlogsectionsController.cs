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
    public class BlogsectionsController : Controller
    {
        private readonly ModelContext _context;

        public BlogsectionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Blogsections
        public async Task<IActionResult> Index()
        {
              return _context.Blogsections != null ? 
                          View(await _context.Blogsections.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Blogsections'  is null.");
        }

        // GET: Blogsections/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Blogsections == null)
            {
                return NotFound();
            }

            var blogsection = await _context.Blogsections
                .FirstOrDefaultAsync(m => m.Blogsectionid == id);
            if (blogsection == null)
            {
                return NotFound();
            }

            return View(blogsection);
        }

        // GET: Blogsections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogsections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Blogsectionid,Name,Tiptitle,Firsttiptext,Secoundtiptext")] Blogsection blogsection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogsection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogsection);
        }

        // GET: Blogsections/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Blogsections == null)
            {
                return NotFound();
            }

            var blogsection = await _context.Blogsections.FindAsync(id);
            if (blogsection == null)
            {
                return NotFound();
            }
            return View(blogsection);
        }

        // POST: Blogsections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Blogsectionid,Name,Tiptitle,Firsttiptext,Secoundtiptext")] Blogsection blogsection)
        {
            if (id != blogsection.Blogsectionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogsection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogsectionExists(blogsection.Blogsectionid))
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
            return View(blogsection);
        }

        // GET: Blogsections/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Blogsections == null)
            {
                return NotFound();
            }

            var blogsection = await _context.Blogsections
                .FirstOrDefaultAsync(m => m.Blogsectionid == id);
            if (blogsection == null)
            {
                return NotFound();
            }

            return View(blogsection);
        }

        // POST: Blogsections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Blogsections == null)
            {
                return Problem("Entity set 'ModelContext.Blogsections'  is null.");
            }
            var blogsection = await _context.Blogsections.FindAsync(id);
            if (blogsection != null)
            {
                _context.Blogsections.Remove(blogsection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogsectionExists(decimal id)
        {
          return (_context.Blogsections?.Any(e => e.Blogsectionid == id)).GetValueOrDefault();
        }
    }
}

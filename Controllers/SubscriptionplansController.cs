using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Hosting;

// Changed To Classes

namespace Fitness_Center_Management.Controllers
{
    public class SubscriptionplansController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubscriptionplansController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment=webHostEnvironment;
        }

        // GET: Subscriptionplans
        public async Task<IActionResult> Index()
        {
            ViewData["TrainerId"] = HttpContext.Session.GetInt32("trainerId");
            return _context.Subscriptionplans != null ? 
                          View(await _context.Subscriptionplans.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Subscriptionplans'  is null.");
        }

        // GET: Subscriptionplans/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Subscriptionplans == null)
            {
                return NotFound();
            }

            var subscriptionplan = await _context.Subscriptionplans
                .FirstOrDefaultAsync(m => m.Planid == id);
            if (subscriptionplan == null)
            {
                return NotFound();
            }

            return View(subscriptionplan);
        }

        // GET: Subscriptionplans/Create
        public IActionResult Create()
        {
            ViewData["TrainerId"] = HttpContext.Session.GetInt32("trainerId");
            return View();
        }

        // POST: Subscriptionplans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Planid,Name,Price,Descripelineone,Descripelinetwo,Descripelinethree,Descripelinefour,ImageFile,Advicetext,Buttontext")] Subscriptionplan subscriptionplan)
        {
            if (ModelState.IsValid)
            {
                if (subscriptionplan.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + subscriptionplan.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await subscriptionplan.ImageFile.CopyToAsync(fileStream);
                    }

                    subscriptionplan.Descripelinefive = fileName;

                }

                _context.Add(subscriptionplan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscriptionplan);
        }

        // GET: Subscriptionplans/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Subscriptionplans == null)
            {
                return NotFound();
            }

            var subscriptionplan = await _context.Subscriptionplans.FindAsync(id);
            if (subscriptionplan == null)
            {
                return NotFound();
            }
            return View(subscriptionplan);
        }

        // POST: Subscriptionplans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Planid,Name,Price,Descripelineone,Descripelinetwo,Descripelinethree,Descripelinefou,ImageFile,Advicetext,Buttontext")] Subscriptionplan subscriptionplan)
        {
            if (id != subscriptionplan.Planid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (subscriptionplan.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + subscriptionplan.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await subscriptionplan.ImageFile.CopyToAsync(fileStream);
                        }

                        subscriptionplan.Descripelinefive = fileName;

                    }

                    _context.Update(subscriptionplan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionplanExists(subscriptionplan.Planid))
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
            return View(subscriptionplan);
        }

        // GET: Subscriptionplans/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Subscriptionplans == null)
            {
                return NotFound();
            }

            var subscriptionplan = await _context.Subscriptionplans
                .FirstOrDefaultAsync(m => m.Planid == id);
            if (subscriptionplan == null)
            {
                return NotFound();
            }

            return View(subscriptionplan);
        }

        // POST: Subscriptionplans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Subscriptionplans == null)
            {
                return Problem("Entity set 'ModelContext.Subscriptionplans'  is null.");
            }
            var subscriptionplan = await _context.Subscriptionplans.FindAsync(id);
            if (subscriptionplan != null)
            {
                _context.Subscriptionplans.Remove(subscriptionplan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionplanExists(decimal id)
        {
          return (_context.Subscriptionplans?.Any(e => e.Planid == id)).GetValueOrDefault();
        }
    }
}

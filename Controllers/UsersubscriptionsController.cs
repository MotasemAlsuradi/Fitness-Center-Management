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
    public class UsersubscriptionsController : Controller
    {
        private readonly ModelContext _context;

        public UsersubscriptionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Usersubscriptions
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Usersubscriptions.Include(u => u.Plan).Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Usersubscriptions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Usersubscriptions == null)
            {
                return NotFound();
            }

            var usersubscription = await _context.Usersubscriptions
                .Include(u => u.Plan)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Subscriptionid == id);
            if (usersubscription == null)
            {
                return NotFound();
            }

            return View(usersubscription);
        }

        // GET: Usersubscriptions/Create
        public IActionResult Create()
        {
            ViewData["Planid"] = new SelectList(_context.Subscriptionplans, "Planid", "Planid");
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid");
            return View();
        }

        // POST: Usersubscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subscriptionid,Userid,Planid,Startdate,Enddate")] Usersubscription usersubscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersubscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Planid"] = new SelectList(_context.Subscriptionplans, "Planid", "Planid", usersubscription.Planid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", usersubscription.Userid);
            return View(usersubscription);
        }

        // GET: Usersubscriptions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Usersubscriptions == null)
            {
                return NotFound();
            }

            var usersubscription = await _context.Usersubscriptions.FindAsync(id);
            if (usersubscription == null)
            {
                return NotFound();
            }
            ViewData["Planid"] = new SelectList(_context.Subscriptionplans, "Planid", "Planid", usersubscription.Planid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", usersubscription.Userid);
            return View(usersubscription);
        }

        // POST: Usersubscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Subscriptionid,Userid,Planid,Startdate,Enddate")] Usersubscription usersubscription)
        {
            if (id != usersubscription.Subscriptionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersubscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersubscriptionExists(usersubscription.Subscriptionid))
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
            ViewData["Planid"] = new SelectList(_context.Subscriptionplans, "Planid", "Planid", usersubscription.Planid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", usersubscription.Userid);
            return View(usersubscription);
        }

        // GET: Usersubscriptions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Usersubscriptions == null)
            {
                return NotFound();
            }

            var usersubscription = await _context.Usersubscriptions
                .Include(u => u.Plan)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Subscriptionid == id);
            if (usersubscription == null)
            {
                return NotFound();
            }

            return View(usersubscription);
        }

        // POST: Usersubscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Usersubscriptions == null)
            {
                return Problem("Entity set 'ModelContext.Usersubscriptions'  is null.");
            }
            var usersubscription = await _context.Usersubscriptions.FindAsync(id);
            if (usersubscription != null)
            {
                _context.Usersubscriptions.Remove(usersubscription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersubscriptionExists(decimal id)
        {
          return (_context.Usersubscriptions?.Any(e => e.Subscriptionid == id)).GetValueOrDefault();
        }
    }
}

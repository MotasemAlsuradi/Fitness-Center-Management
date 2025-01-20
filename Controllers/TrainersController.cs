using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Hosting;

namespace Fitness_Center_Management.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TrainersController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
              return _context.Trainers != null ? 
                          View(await _context.Trainers.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Trainers'  is null.");
        }

        // GET: Trainers/Search
        [HttpGet]
        public IActionResult Search()
        {
            // Return the search view
            return View();
        }

        // POST: Trainers/Search
        [HttpPost]
        public async Task<IActionResult> Search(string? searchQuery)
        {
            // Check if the search query is null or empty
            if (string.IsNullOrEmpty(searchQuery))
            {
                // If no query, return all trainers
                var allTrainers = await _context.Trainers.ToListAsync();
                return View("Index", allTrainers);  // Return to the Index view
            }

            // Otherwise, filter trainers based on search query (FirstName, LastName, or Username)
            var filteredTrainers = await _context.Trainers
                .Where(t => t.Firstname.ToLower().Contains(searchQuery.ToLower()) ||
                            t.Lastname.ToLower().Contains(searchQuery.ToLower()) ||
                            t.Username.ToLower().Contains(searchQuery.ToLower()))
                .ToListAsync();

            // Return the filtered list of trainers
            return View("Index", filteredTrainers);  // Pass the filtered trainers to the same Index view
        }


        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.Trainersid == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: Ttainers/Members
        public async Task<IActionResult> Members()
        {
            ViewData["TrainerId"] = HttpContext.Session.GetInt32("trainerId");
            return _context.Users != null ?
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Users'  is null.");
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Trainersid,Firstname,Lastname,Expertise,PictureUrl,Socialfacebook,Socialtwitter,Sociallinkedin,Username,Password")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                if (trainer.PictureUrl != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + trainer.PictureUrl.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await trainer.PictureUrl.CopyToAsync(fileStream);
                    }
                    trainer.Picture = fileName;
                }

                _context.Add(trainer);

                Userlogin userlogin = new Userlogin
                {
                    Roleid = 2,
                    Username = trainer.Username,
                    Password = trainer.Password
                };
                _context.Add(userlogin);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/2
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["TrainerId"] = HttpContext.Session.GetInt32("trainerId");
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/2
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Trainersid,Firstname,Lastname,Expertise,PictureUrl,Socialfacebook,Socialtwitter,Sociallinkedin,Username,Password")] Trainer trainer)
        {
            if (id != trainer.Trainersid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing admin from the database
                    var existingTrainer = await _context.Trainers.AsNoTracking().FirstOrDefaultAsync(t => t.Trainersid == id);

                    if (existingTrainer == null)
                    {
                        return NotFound();
                    }

                    if (trainer.PictureUrl != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + trainer.PictureUrl.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await trainer.PictureUrl.CopyToAsync(fileStream);
                        }

                        trainer.Picture = fileName;

                    }

                    // Find and update the corresponding Userlogin record if it exists
                    var userlogin = await _context.Userlogins.FirstOrDefaultAsync(u => u.Username == existingTrainer.Username);
                    if (userlogin != null)
                    {
                        // Update the Admin entity
                        _context.Update(trainer);

                        userlogin.Username = trainer.Username;
                        userlogin.Password = trainer.Password;

                        _context.Update(userlogin);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Trainersid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new {id});
            }

            return View(trainer);
        }


        // GET: Trainers/Delete/2
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.Trainersid == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Trainers/Delete/2
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Trainers == null)
            {
                return Problem("Entity set 'ModelContext.Trainers'  is null.");
            }

            var trainer = await _context.Trainers.FindAsync(id);
            
            if (trainer != null)
            {
                var userlogin = await _context.Userlogins.FirstOrDefaultAsync(u => u.Username == trainer.Username);
                if (userlogin != null)
                {
                    _context.Userlogins.Remove(userlogin);
                }

                _context.Trainers.Remove(trainer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(decimal id)
        {
          return (_context.Trainers?.Any(e => e.Trainersid == id)).GetValueOrDefault();
        }
    }
}

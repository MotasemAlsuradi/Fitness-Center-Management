using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center_Management.Models;
using NuGet.DependencyResolver;

namespace Fitness_Center_Management.Controllers
{
    public class UsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Users'  is null.");
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
                var allUsers = await _context.Users.ToListAsync();
                return View("Index", allUsers);  // Return to the Index view
            }

            // Otherwise, filter trainers based on search query (FirstName, LastName, or Username)
            var filteredUsers = await _context.Users
                .Where(t => t.Firstname.ToLower().Contains(searchQuery.ToLower()) ||
                            t.Lastname.ToLower().Contains(searchQuery.ToLower()) ||
                            t.Username.ToLower().Contains(searchQuery.ToLower()))
                .ToListAsync();

            // Return the filtered list of trainers
            return View("Index", filteredUsers);  // Pass the filtered trainers to the same Index view
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Firstname,Lastname,Email,Phonenumber,Age,Gender,Picture,Username,Password,Registrationdate")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PictureUrl != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.PictureUrl.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.PictureUrl.CopyToAsync(fileStream);
                    }

                    user.Picture = fileName;
                }

                _context.Add(user);

                Userlogin userlogin = new Userlogin
                {
                    Roleid = 3,
                    Username = user.Username,
                    Password = user.Password
                };

                _context.Add(userlogin);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Firstname,Lastname,Email,Phonenumber,Age,Gender,Picture,Username,Password,Registrationdate")] User user)
        {
            if (id != user.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Userid))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ModelContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
          return (_context.Users?.Any(e => e.Userid == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Fitness_Center_Management.Services;

namespace Fitness_Center_Management.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public readonly IPagesViewService _pagesViewService;

        public AdminsController(ModelContext context, IWebHostEnvironment webHostEnviroment, IPagesViewService pagesViewService)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
            _pagesViewService = pagesViewService;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return _context.Admins != null ?
                View(await _context.Admins.ToListAsync()) :
                Problem("Entity set 'ModelContext.Admins' is null.");
        }

        // GET: Admins/Details/5
        /*
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Adminid == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }*/

        // GET: Admins/Create
        /*
        public IActionResult Create()
        {
            return View();
        }
        */

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Create([Bind("Adminid,Firstname,Lastname,Email,Picture,Username,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);

                // Create a new Userlogin entity and set its properties
                Userlogin userlogin = new Userlogin
                {
                    Roleid = 1,
                    Username = admin.Username,
                    Password = admin.Password
                };
                _context.Add(userlogin);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        } */

        // GET: Admins/Edit/1
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        } 

        // POST: Admins/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Adminid,Firstname,Lastname,Email,PictureUrl,Username,Password")] Admin admin)
        {
            if (id != admin.Adminid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing admin from the database
                    var existingAdmin = await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Adminid == id);

                    if (existingAdmin == null)
                    {
                        return NotFound();
                    }

                    if (admin.PictureUrl != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + admin.PictureUrl.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await admin.PictureUrl.CopyToAsync(fileStream);
                        }

                        admin.Picture = fileName;

                    }

                    // Update the Admin entity
                    _context.Update(admin);

                    // Find and update the corresponding Userlogin record if it exists
                    var userlogin = await _context.Userlogins.FirstOrDefaultAsync(u => u.Username == existingAdmin.Username);
                    if (userlogin != null)
                    {
                        userlogin.Username = admin.Username;
                        userlogin.Password = admin.Password;

                        _context.Update(userlogin);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Adminid))
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

            return View(admin);
        }

        /*

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Adminid == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'ModelContext.Admins'  is null.");
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                var userlogin = await _context.Userlogins.FirstOrDefaultAsync(u => u.Username == admin.Username);
                if (userlogin != null)
                {
                    _context.Userlogins.Remove(userlogin);
                }

                _context.Admins.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        */

        private bool AdminExists(decimal id)
        {
          return (_context.Admins?.Any(e => e.Adminid == id)).GetValueOrDefault();
        }
    }
}

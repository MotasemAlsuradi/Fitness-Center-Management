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
    public class LandingsectionsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LandingsectionsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Landingsections != null ? 
                          View(await _context.Landingsections.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Landingsections'  is null.");
        }

        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Landingsections == null)
            {
                return NotFound();
            }

            var landingsection = await _context.Landingsections.FindAsync(id);
            if (landingsection == null)
            {
                return NotFound();
            }
            return View(landingsection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Landingsectionid,Name,ImageFile,Firstwelcometext,Secoundwelcometext,Firstbutton,Secoundbutton")] Landingsection landingsection)
        {
            if (id != landingsection.Landingsectionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (landingsection.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + landingsection.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await landingsection.ImageFile.CopyToAsync(fileStream);
                        }

                        landingsection.Mainimage = fileName;

                    }

                    _context.Update(landingsection);
                    await _context.SaveChangesAsync();
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandingsectionExists(landingsection.Landingsectionid))
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
            return View(landingsection);
        }


        private bool LandingsectionExists(decimal id)
        {
          return (_context.Landingsections?.Any(e => e.Landingsectionid == id)).GetValueOrDefault();
        }
    }
}

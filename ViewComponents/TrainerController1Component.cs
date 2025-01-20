using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Center_Management.ViewComponents
{
    public class TrainerController1Component : ViewComponent
    {
        private readonly ModelContext _context;

        public TrainerController1Component(ModelContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["trainerId"] = HttpContext.Session.GetInt32("trainerId");
            int? trainerId = HttpContext.Session.GetInt32("trainerId");
            if (trainerId == null)
            {
                
                return View(null);
            }

            var trainer = await _context.Trainers
                .Where(t => t.Trainersid == trainerId)
                .SingleOrDefaultAsync();

            return View(trainer);
        }
    }
}

using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Center_Management.ViewComponents
{
    public class AdminController1Component:ViewComponent
    {
        private readonly ModelContext _context;

        public AdminController1Component(ModelContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var adminInfo = await _context.Admins.FirstOrDefaultAsync();
            return View(adminInfo);
        }
    }
}

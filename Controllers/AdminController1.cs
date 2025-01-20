using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Center_Management.Controllers
{
    public class AdminController1 : Controller
    {
        private readonly ModelContext _context;

        public AdminController1(ModelContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            ViewBag.TotalMembers = _context.Users.Count();
            ViewBag.Subscriptions = _context.Usersubscriptions.Count();
            ViewData["adminFullName"] = HttpContext.Session.GetString("adminFullName");
            
            var viewModel = new AdminDashboardViewModel
            {
                ContactForm = await _context.Contactforms.ToListAsync(),
                Admins = await _context.Admins.ToListAsync(),
                feedbacks = await _context.Feedbacks.ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult Documentation() 
        { 
            return View();
        }
    }
}

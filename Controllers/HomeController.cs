using Fitness_Center_Management.Models;
using Fitness_Center_Management.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fitness_Center_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IPagesViewService _pagesViewService;

        private PagesViewModel GetPagesViewModel()
        {
            return _pagesViewService.GetPagesViewModel();
        }

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment, IPagesViewService pagesViewService)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _pagesViewService = pagesViewService;
        }
        
        public IActionResult Index()
        {
            ViewData["userId"] = HttpContext.Session.GetInt32("userId");
            ViewBag.UserPic = HttpContext.Session.GetString("userPic");
            ViewBag.UserFullName = HttpContext.Session.GetString("userFullName");
            return View(GetPagesViewModel());
        }

        public IActionResult About()
        {
            ViewData["userId"] = HttpContext.Session.GetInt32("userId");
            ViewBag.UserPic = HttpContext.Session.GetString("userPic");
            ViewBag.UserFullName = HttpContext.Session.GetString("userFullName");
            return View(GetPagesViewModel());
        }

        public IActionResult Classes()
        {
            ViewData["userId"] = HttpContext.Session.GetInt32("userId");
            ViewBag.UserPic = HttpContext.Session.GetString("userPic");
            ViewBag.UserFullName = HttpContext.Session.GetString("userFullName");
            return View(GetPagesViewModel());
        }

        public IActionResult Schedules()
        {
            ViewData["userId"] = HttpContext.Session.GetInt32("userId");
            ViewBag.UserPic = HttpContext.Session.GetString("userPic");
            ViewBag.UserFullName = HttpContext.Session.GetString("userFullName");
            return View(GetPagesViewModel());
        }

        public IActionResult Login()
        {
            return View(GetPagesViewModel());
        }

        [HttpPost]
        public IActionResult Login([Bind("Username, Password")] Userlogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Username) || string.IsNullOrEmpty(userLogin.Password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View();
            }

            var auth = _context.Userlogins
                .Where(user => user.Username.ToLower() == userLogin.Username.ToLower()
                         && user.Password == userLogin.Password)
                .SingleOrDefault();

            if (auth != null)
            {
                switch (auth.Roleid)
                {
                    case 1:
                        var adminFullName = _context.Admins
                            .Select(admin => new { admin.Firstname, admin.Lastname })
                            .FirstOrDefault();

                        if (adminFullName != null)
                        {
                            HttpContext.Session.SetString("adminFullName", $"{adminFullName.Firstname} {adminFullName.Lastname}");
                        }
                        return RedirectToAction("Index", "AdminController1");
                    
                    case 2:
						var trainer = _context.Trainers
					        .Where(t => t.Username.ToLower() == userLogin.Username.ToLower())
					        .SingleOrDefault();

						if (trainer != null)
                        {
                            HttpContext.Session.SetInt32("trainerId", Convert.ToInt32(trainer.Trainersid));
							HttpContext.Session.SetString("trainerFullName", $"{trainer.Firstname} {trainer.Lastname}");
						}

						return RedirectToAction("Index", "TrainerController1");

                    case 3:
                        var user = _context.Users
                            .Where(u => u.Username.ToLower() == userLogin.Username.ToLower())
                            .SingleOrDefault();

                        if (user != null)
                        {
                            HttpContext.Session.SetInt32("userId", Convert.ToInt32(user.Userid));
                            HttpContext.Session.SetString("userFullName", $"{user.Firstname} {user.Lastname}");
                            HttpContext.Session.SetString("userPic", user.Picture);
                        }

                        return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        //public IActionResult MemberProfile() 
        //{
        //    ViewData["userId"] = HttpContext.Session.GetInt32("userId");
        //    ViewBag.UserPic = HttpContext.Session.GetString("userPic");
        //    ViewBag.UserFullName = HttpContext.Session.GetString("userFullName");
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitMessage(string name, string email, string message)
        {
            Contactform contactform = new Contactform()
            { 
                Name = name,
                Email = email,
                Message = message
            };
            
            _context.Add(contactform);
            await _context.SaveChangesAsync();

            TempData["MessageSuccess"] = "Your message has been successfully sent. We will contact you shortly.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

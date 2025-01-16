using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Hosting;
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

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Subscriptions()
        {
            return View();
        }

        public IActionResult Schedules()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
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
                        return RedirectToAction("Index", "AdminController1");
                }
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync([Bind("Userid,Firstname,Lastname,Email,Phonenumber,Age,Gender,PictureUrl,Username,Password,Registrationdate")] User user)
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

                return RedirectToAction(nameof(Login));
            }

            return View(user);
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

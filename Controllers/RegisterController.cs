using Fitness_Center_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Fitness_Center_Management.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context=context;
            _webHostEnvironment=webHostEnvironment;
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

                return RedirectToAction("Login", "Home");
            }

            return View(user);
        }
    }
}

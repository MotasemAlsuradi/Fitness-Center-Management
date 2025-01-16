using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center_Management.Controllers
{
    public class AdminController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

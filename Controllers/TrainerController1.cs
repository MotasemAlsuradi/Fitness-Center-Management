using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center_Management.Controllers
{
    public class TrainerController1 : Controller
    {
        public IActionResult Index()
        {
            ViewData["TrainerId"] = HttpContext.Session.GetInt32("trainerId");
            ViewData["trainerFullName"] = HttpContext.Session.GetString("trainerFullName");
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Logique.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logique.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
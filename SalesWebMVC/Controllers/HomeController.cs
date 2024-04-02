using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models.ViewModels;
using System.Diagnostics;

namespace SalesWebMVC.Controllers {
    public class HomeController(ILogger<HomeController> logger) : Controller {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {
            ViewData["Professor"] = "Nelio Alves";
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

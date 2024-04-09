using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers {
    public class SellersController(ServiceSeller serviceSeller) : Controller {
        private readonly ServiceSeller _serviceSeller = serviceSeller;

        public IActionResult Index() {
            var listSellers = _serviceSeller.FindAll();
            return View(listSellers);
        }
    }
}

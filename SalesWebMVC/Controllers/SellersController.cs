using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers {
    public class SellersController(SellerService serviceSeller, DepartmentService serviceDepartment) : Controller {
        private readonly SellerService _serviceSeller = serviceSeller;
        private readonly DepartmentService _serviceDepartment = serviceDepartment;

        public IActionResult Index() {
            var listSellers = _serviceSeller.FindAll();
            return View(listSellers);
        }

        public IActionResult Create() {
            var departments = _serviceDepartment.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            _serviceSeller.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}

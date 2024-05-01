using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using System.Diagnostics;

namespace SalesWebMVC.Controllers {
    public class SellersController(SellerService sellerService, DepartmentService departmentService) : Controller {
        private readonly SellerService _sellerService = sellerService;
        private readonly DepartmentService _departmentService = departmentService;

        public IActionResult Index() {
            var listSellers = _sellerService.FindAll();
            return View(listSellers);
        }

        public IActionResult Create() {
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new() { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            if (!ModelState.IsValid) return View(new SellerFormViewModel { Seller = seller, Departments = _departmentService.FindAll() });
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not informed." });
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            return View(seller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id informed." });
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            return View(seller);
        }

        public IActionResult Edit(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not informed." });
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new() { Departments = departments, Seller = seller };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Seller Id mismatch." });
            if (!ModelState.IsValid) return View(new SellerFormViewModel { Seller = seller, Departments = _departmentService.FindAll() });
            try {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message) {
            ErrorViewModel errorViewModel = new() {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }
    }
}

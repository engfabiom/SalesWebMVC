using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

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
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) {
            if (id == null) return NotFound();
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return NotFound();
            return View(seller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) return NotFound();
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return NotFound();
            return View(seller);
        }

        public IActionResult Edit(int? id) {
            if (id == null) return NotFound();
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) return NotFound();
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            if (id != seller.Id) return BadRequest();
            try {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e) {
                return NotFound();
            }
            catch (DbConcurrencyException e) {
                return BadRequest();
            }
        }
    }
}

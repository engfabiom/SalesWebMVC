using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers {
    public class SellersController(SellerService sellerService, DepartmentService departmentService) : Controller {
        private readonly SellerService _sellerService = sellerService;
        private readonly DepartmentService _departmentService = departmentService;

        public async Task<IActionResult> Index() {
            var listSellers = await _sellerService.FindAllAsync();
            return View(listSellers);
        }

        public async Task<IActionResult> Create() {
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new() { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) {
            if (!ModelState.IsValid) return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() });
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not informed." });
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            return View(seller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            try {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e) {
                return RedirectToAction(nameof(Error), new { message = "Seller cannot be deleted as there are Sales registered." });
            }

        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not informed." });
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not informed." });
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Seller Id not found." });
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new() { Departments = departments, Seller = seller };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller) {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Seller Id mismatch." });
            if (!ModelState.IsValid) return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() });
            try {
                await _sellerService.UpdateAsync(seller);
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

using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers {
    public class DepartmentsController : Controller {
        public IActionResult Index() {
            List<Department> departments = new List<Department>();
            departments.Add(new Department { Id = 1, Name = "Electronics" });
            departments.Add(new Department { Id = 2, Name = "Fashion" });

            return View(departments);
        }
    }
}

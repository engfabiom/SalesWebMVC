using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services {
    public class DepartmentService(SalesWebMVCContext context) {
        private SalesWebMVCContext _context = context;

        public List<Department> FindAll() {
            return _context.Department.OrderBy(d => d.Name).ToList();
        }
    }
}

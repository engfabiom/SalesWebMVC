using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services {
    public class DepartmentService(SalesWebMVCContext context) {
        private readonly SalesWebMVCContext _context = context;

        public async Task<List<Department>> FindAllAsync() {
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}

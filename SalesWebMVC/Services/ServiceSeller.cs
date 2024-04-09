using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services {
    public class ServiceSeller(SalesWebMVCContext context) {
        private readonly SalesWebMVCContext _context = context;
        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }
    }
}

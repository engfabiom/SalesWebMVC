using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services {
    public class SellerService(SalesWebMVCContext context) {
        private readonly SalesWebMVCContext _context = context;
        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public void Insert(Seller newSeller) {
            _context.Add(newSeller);
            _context.SaveChanges();
        }
    }
}
using Microsoft.EntityFrameworkCore;
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

        public Seller FindById(int id) {
            return _context.Seller.Include(d => d.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id) {
            var seller = FindById(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}
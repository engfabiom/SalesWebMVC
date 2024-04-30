using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

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

        public void Update(Seller seller) {
            if (!_context.Seller.Any(s => s.Id == seller.Id)) throw new NotFoundException("Seller ID not found.");
            try {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
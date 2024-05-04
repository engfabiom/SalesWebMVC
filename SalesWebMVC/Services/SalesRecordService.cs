using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services {
    public class SalesRecordService(SalesWebMVCContext context) {
        private readonly SalesWebMVCContext _context = context;

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {

            IQueryable<SalesRecord> sales = from salesRecords in _context.SalesRecord select salesRecords;

            if (minDate.HasValue) sales = sales.Where(s => s.Date >= minDate.Value);
            if (maxDate.HasValue) sales = sales.Where(s => s.Date <= maxDate.Value);

            return await sales
                .Include(t => t.Seller)
                .Include(t => t.Seller.Department)
                .OrderByDescending(f => f.Date)
                .ToListAsync();
        }
    }
}
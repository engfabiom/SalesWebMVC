using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data {
    public class SalesWebMVCContext : DbContext {
        public SalesWebMVCContext(DbContextOptions<SalesWebMVCContext> options)
            : base(options) {
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<SalesRecord>().ToTable("SalesRecord");
        }
    }
}

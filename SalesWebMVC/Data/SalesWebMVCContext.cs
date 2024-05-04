using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data {
    public class SalesWebMVCContext(DbContextOptions<SalesWebMVCContext> options) : DbContext(options) {
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<SalesRecord>().ToTable("SalesRecord");

            modelBuilder.Entity<SalesRecord>()
                .HasOne(f => f.Seller)
                .WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

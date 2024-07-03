using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> company { get; set; }
        public DbSet<Product> product { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
               .HasMany(e => e.Products)
               .WithOne(e => e.Company)
               .HasForeignKey(e => e.CompanyId)
               .IsRequired();

        }
    }
}

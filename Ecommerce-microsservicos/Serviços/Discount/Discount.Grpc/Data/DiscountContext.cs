using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Cupom> Cupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cupom>().HasData(
                new Cupom { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Cupom { Id = 2, ProductName = "Samsung S20", Description = "Samsung Discount", Amount = 100 }
                );
        }
    }
}

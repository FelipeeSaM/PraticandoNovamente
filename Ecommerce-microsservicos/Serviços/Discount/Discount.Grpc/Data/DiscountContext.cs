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
    }
}

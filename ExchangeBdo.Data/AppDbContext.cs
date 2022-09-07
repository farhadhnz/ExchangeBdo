using ExchangeBdo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBdo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Symbol> Symbols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exchange>().Property(o => o.Rate).HasPrecision(20, 10);

            base.OnModelCreating(modelBuilder);
        }
    }
}

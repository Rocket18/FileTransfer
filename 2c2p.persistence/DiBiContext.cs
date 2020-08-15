using _2c2p.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2c2p.persistence
{
    public class DiBiContext : DbContext
    {
        public DiBiContext(DbContextOptions<DiBiContext> options)
         : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<CurrencyCode> CurrencyCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.TransactionId).IsUnique();
            });
        }
    }
}

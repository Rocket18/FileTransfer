using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace _2c2p.persistence
{
    public class DibiContextFactory : IDesignTimeDbContextFactory<DiBiContext>
    {
        DiBiContext IDesignTimeDbContextFactory<DiBiContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DiBiContext>();

            optionsBuilder.UseSqlite("Data Source=Transaction.db");

            return new DiBiContext(optionsBuilder.Options);
        } 
    }
}

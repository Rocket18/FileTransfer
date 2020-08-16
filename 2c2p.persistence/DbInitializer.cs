using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _2c2p.persistence
{
    public class DbInitializer
    {
        private readonly IApplicationBuilder _app;

        public DbInitializer(IApplicationBuilder app)
        {
            _app = app;
        }

        /// <summary>
        /// Apply Db migrations
        /// </summary>
        public void UpdateDatabase()
        {
            using var serviceScope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<DiBiContext>().Database.Migrate();
        }
    }
}

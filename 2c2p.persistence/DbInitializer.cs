using _2c2p.domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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
        /// Insert some data in database
        /// </summary>
        public void SeedEverything()
        {
            using var serviceScope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<DiBiContext>();

            context.Database.EnsureCreated();

            var count = context.CurrencyCodes.Count();

            if (count == default)
            {
                InserCurrencyCodes(context);
            }

        }


        /// <summary>
        /// Apply Db migrations
        /// </summary>
        public void UpdateDatabase()
        {
            using var serviceScope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<DiBiContext>().Database.Migrate();
        }

        private void InserCurrencyCodes(DiBiContext context)
        {
            var codes = new List<CurrencyCode>() {

                 new CurrencyCode() {Code = "EUR" },
                 new CurrencyCode() { Code = "USD" }
                };

            context.CurrencyCodes.AddRange(codes);

            context.SaveChanges();
        }
    }
}

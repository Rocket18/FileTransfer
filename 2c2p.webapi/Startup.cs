using _2c2p.application.Contracts;
using _2c2p.application.Enumerations;
using _2c2p.application.Services;
using _2c2p.infrastructure.Services;
using _2c2p.persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _2c2p.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO Uncomment and configure to accept request from other domains (for production)
            // services.AddCors();

            // P.S Never commit appSettings.json with configuration im github repo, it's unsecured
            // Better get configuration form Azure Key Vault for example
            services.AddDbContext<DiBiContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IFileImportService, FileImportService>();

            services.AddTransient<IValidationService, TransactionValidationService>();
  
            services.AddTransient<Func<FileType, IFileService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case FileType.Xml:
                        return serviceProvider.GetService<XmlFileService>();
                    case FileType.Csv:
                        return serviceProvider.GetService<CsvFileService>();
                    default:
                        return null;
                }
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Good place to apply db migrations after application start (or u can do this in last CI/CD step)
            new DbInitializer(app).UpdateDatabase();

            //new DbInitializer(app).SeedEverything();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

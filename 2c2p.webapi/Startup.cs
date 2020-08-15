using _2c2p.application.Contracts;
using _2c2p.application.Enumerations;
using _2c2p.application.Services;
using _2c2p.domain.Models;
using _2c2p.infrastructure.Services;
using _2c2p.persistence;
using _2c2p.webapi.Configuration;
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
            services.AddCors(options => new CorsConfiguration(Configuration).SetConfiguration(options));

            // P.S Never commit appSettings.json with configuration im github repo, it's unsecured
            // Better get configuration form Azure Key Vault for example
            services.AddDbContext<DiBiContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IFileImportService, FileImportService>();

            services.AddTransient<IValidationService<TransactionModel>, TransactionValidationService>();

            services.AddTransient<XmlFileService>();
            services.AddTransient<CsvFileService>();

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

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Good place to apply db migrations after application start (or u can do this in last CI/CD step)
            new DbInitializer(app).UpdateDatabase();

            // Insert Currency codes in table (for task purpose only)
            // Comment this line for production or any other DB
            new DbInitializer(app).SeedEverything();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });

            app.UseRouting();

            app.UseCors(CorsConfiguration.CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

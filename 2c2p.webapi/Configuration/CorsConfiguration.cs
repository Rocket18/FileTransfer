using _2c2p.webapi.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace _2c2p.webapi.Configuration
{
    public class CorsConfiguration
    {
        public static string SectionName = "CorsConfiguration";

        private readonly IConfiguration _configuration;

        public static string CorsPolicyName = "CorsPolicy";


        public CorsConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SetConfiguration(CorsOptions options)
        {
            var settings = _configuration.GetSection(SectionName).Get<CorsSettings>();

            options.AddPolicy(CorsPolicyName,
                    builder => builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(settings.Origins)
                    .AllowCredentials());
        }
    }
}

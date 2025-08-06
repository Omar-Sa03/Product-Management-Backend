#nullable disable
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repositories;

namespace API.Extension
{
    public static class ContextExtension
    {
        public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DematContext>(options => options.UseNpgsql(GetConnectionInfo(configuration).ToString()).EnableSensitiveDataLogging());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Register Data Access Layer
            services.AddScoped<IDematContext,DematContext>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IPerfumeRepository, PerfumeRepository>();
        }

        private static DbConnectionInfo GetConnectionInfo(IConfiguration configuration)
        {
            DbConnectionInfo dbConnectionInfo;

            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                dbConnectionInfo = new()
                {
                    Host = Environment.GetEnvironmentVariable("PG_HOST"),
                    Database = Environment.GetEnvironmentVariable("PG_DATABASE"),
                    Username = Environment.GetEnvironmentVariable("PG_USERNAME"),
                    Password = Environment.GetEnvironmentVariable("PG_PASSWORD")
                };
            }
            else
            {
                dbConnectionInfo = new()
                {
                    Host = configuration.GetValue<string>("DataConnection:Hostname"),
                    Database = configuration.GetValue<string>("DataConnection:Database"),
                    Username = configuration.GetValue<string>("DataConnection:Username"),
                    Password = configuration.GetValue<string>("DataConnection:Password")
                };
            }

            return dbConnectionInfo;
        }
    }
}

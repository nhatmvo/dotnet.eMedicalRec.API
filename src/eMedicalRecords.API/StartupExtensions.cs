using System;
using System.Reflection;
using eMedicalRecords.Infrastructure;
using eMedicalRecords.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace eMedicalRecords.API
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfiguration = configuration.Get<DbConfiguration>();
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbConfiguration.Hostname,
                Database = dbConfiguration.Database,
                Username = dbConfiguration.Username,
                Password = dbConfiguration.Password,
                Port = int.Parse(dbConfiguration.Port)
            };
            services.AddDbContext<MedicalRecordContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null!);
                });
            });
            
            return services;
        }
    }
}
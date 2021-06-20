using System;
using System.Reflection;
using eMedicalRecords.API.Projections;
using eMedicalRecords.Infrastructure;
using eMedicalRecords.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
                Host = dbConfiguration.PostgresHostname,
                Database = dbConfiguration.PostgresDatabase,
                Username = dbConfiguration.PostgresUsername,
                Password = dbConfiguration.PostgresPassword,
                Port = int.Parse(dbConfiguration.PostgresPort)
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

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            return services;
        } 

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddCustomHostedService(this IServiceCollection services)
        {
            services.AddHostedService<ProjectionStartupService>();
            return services;
        }
        
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DbConfiguration>(configuration);
            services.Configure<TemplateDatabaseSettings>(configuration.GetSection(nameof(TemplateDatabaseSettings)));
            
            services.AddSingleton<ITemplateDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TemplateDatabaseSettings>>().Value);
            
            return services;
        }
    }
}
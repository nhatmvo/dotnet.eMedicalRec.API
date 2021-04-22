using System;
using System.Reflection;
using eMedicalRecords.Infrastructure;
using eMedicalRecords.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StackExchange.Redis;

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

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            return services;
        } 

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
        
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DbConfiguration>(configuration);
            services.Configure<RedisConfiguration>(configuration);
            return services;
        }

        public static IServiceCollection AddCustomMultiplexer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var settings = configuration.Get<RedisConfiguration>();
                var confOptions = ConfigurationOptions.Parse(settings.RedisConnectionString, true);

                confOptions.ResolveDns = true;

                return ConnectionMultiplexer.Connect(confOptions);
            });

            return services;
        }
    }
}
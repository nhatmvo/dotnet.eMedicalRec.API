using System.Reflection;
using Autofac;
using eMedicalRecords.API.Infrastructures.AutofacModules;
using eMedicalRecords.API.Infrastructures.Middlewares;
using eMedicalRecords.Infrastructure.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace eMedicalRecords.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                .AddCustomConfiguration(Configuration)
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddCustomDbContext(Configuration)
                .AddCustomHostedService()
                .AddJwt()
                .AddCustomHealthCheck(Configuration);
            
            services.AddCors();

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var dbConfiguration = Configuration.Get<DbConfiguration>();
            var npgsqlBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbConfiguration.PostgresHostname,
                Database = dbConfiguration.PostgresDatabase,
                Username = dbConfiguration.PostgresUsername,
                Password = dbConfiguration.PostgresPassword,
                Port = int.Parse(dbConfiguration.PostgresPort)
            };

            
            builder.RegisterModule(new ApplicationModules(npgsqlBuilder.ConnectionString));
            builder.RegisterModule(new MediatorModules());
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
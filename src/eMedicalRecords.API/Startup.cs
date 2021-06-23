using System.Reflection;
using Autofac;
using eMedicalRecords.API.Infrastructures.AutofacModules;
using eMedicalRecords.API.Infrastructures.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModules>();
            builder.RegisterModule<MediatorModules>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
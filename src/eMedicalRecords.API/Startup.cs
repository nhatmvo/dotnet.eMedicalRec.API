using System.Reflection;
using Autofac;
using eMedicalRecords.API.Infrastructures.AutofacModules;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddCustomHealthCheck(Configuration);

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModules>();
            builder.RegisterModule<MediatorModules>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
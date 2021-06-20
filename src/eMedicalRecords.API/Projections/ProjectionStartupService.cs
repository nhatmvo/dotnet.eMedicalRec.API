using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eMedicalRecords.API.Projections
{
    public class ProjectionStartupService : IHostedService
    {
        private readonly IStateProjection _projection;
        private readonly IServiceProvider _serviceProvider;

        public ProjectionStartupService(IStateProjection projection, IServiceProvider serviceProvider)
        {
            _projection = projection;
            _serviceProvider = serviceProvider;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            _projection.SubscribeTemplateState();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
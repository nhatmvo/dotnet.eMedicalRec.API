using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace eMedicalRecords.API.Applications.DomainEventHandlers
{
    public class TemplateAddedDomainEventHandler : INotificationHandler<TemplateAddedDomainEvent>
    {
        private readonly ITemplateRedisRepository _redisRepository;
        private readonly ILogger<TemplateAddedDomainEventHandler> _logger;

        public TemplateAddedDomainEventHandler(ITemplateRedisRepository redisRepository,
            ILogger<TemplateAddedDomainEventHandler> logger)
        {
            _redisRepository = redisRepository;
            _logger = logger;
        }
        
        public async Task Handle(TemplateAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var structure = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(notification.Template),
                cancellationToken);
            await _redisRepository.UpsertTemplateAsync(notification.TemplateId, structure);
            
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.DomainEventHandlers
{
    using Domain.AggregatesModel.DocumentAggregate;
    using Domain.Events;
    
    public class PatientAddedDomainEventHandler : INotificationHandler<PatientAddedDomainEvent>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<PatientAddedDomainEventHandler> _logger;
        public PatientAddedDomainEventHandler(IDocumentRepository documentRepository, 
            ILogger<PatientAddedDomainEventHandler> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }
        
        public Task Handle(PatientAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("====== Create document for patient {PatientId}", notification.PatientId);
            _documentRepository.Add(new Document( "Orthopedic", notification.PatientId));
            return Task.CompletedTask;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.Events;
using MediatR;

namespace eMedicalRecords.API.Applications.DomainEventHandlers
{
    public class PatientAddedDomainEventHandler : INotificationHandler<PatientAddedDomainEvent>
    {
        public Task Handle(PatientAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO: Patient Added - Sending email or sms...
            return Task.CompletedTask;
        }
    }
}
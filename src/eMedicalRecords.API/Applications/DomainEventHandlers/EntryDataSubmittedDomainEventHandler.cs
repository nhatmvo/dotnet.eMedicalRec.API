using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.DomainEventHandlers
{
    public class EntryDataSubmittedDomainEventHandler : INotificationHandler<EntryDataSubmittedDomainEvent>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ILogger<EntryDataSubmittedDomainEventHandler> _logger;

        public EntryDataSubmittedDomainEventHandler(ITemplateRepository templateRepository,
            ILogger<EntryDataSubmittedDomainEventHandler> logger)
        {
            _templateRepository = templateRepository;
            _logger = logger;
        }
        
        public async Task Handle(EntryDataSubmittedDomainEvent notification, CancellationToken cancellationToken)
        {
            var availableOptions = await _templateRepository.GetAvailableSectionOptions(notification.SectionId);
            if (availableOptions.Any() &&
                !availableOptions.Any(p => p.Equals(notification.Value, StringComparison.CurrentCultureIgnoreCase)))
            {
                _logger.LogError("Cannot find submitted value {@Value} inside available options. Section: {@SectionId}",
                    notification.Value, notification.SectionId);
                throw new Exception();
            }

            var control = await _templateRepository.GetControlTypeBySectionId(notification.SectionId);
            switch (control)
            {
                case ControlText c:
                    c.ValidateMaximumLength(notification.Value);
                    c.ValidateMinimumLength(notification.Value);
                    c.ValidateTextRestrictionLevel(notification.Value);
                    c.ValidateWithCustomExpression(notification.Value);
                    break;
            }


        }
    }
}
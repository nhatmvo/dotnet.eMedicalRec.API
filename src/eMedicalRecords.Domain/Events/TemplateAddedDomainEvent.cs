using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using MediatR;

namespace eMedicalRecords.Domain.Events
{
    public class TemplateAddedDomainEvent : INotification
    {
        public string TemplateId { get; set; }
        public Template Template { get; set; }

        public TemplateAddedDomainEvent(Template template, string templateId)
        {
            Template = template;
            TemplateId = templateId;
        }
    }
}
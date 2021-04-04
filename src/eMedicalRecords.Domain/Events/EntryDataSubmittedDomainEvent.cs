using System;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace eMedicalRecords.Domain.Events
{
    public class EntryDataSubmittedDomainEvent : INotification
    {
        public string Value { get; }
        public Guid SectionId { get; }
        public EntryData EntryData { get; }

        public EntryDataSubmittedDomainEvent(EntryData entryData, Guid sectionId, string value)
        {
            Value = value;
            SectionId = sectionId;
            EntryData = entryData;
        }
    }
}
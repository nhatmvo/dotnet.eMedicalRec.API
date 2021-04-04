using System;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class EntryData : Entity
    {
        private Guid _sectionId;

        private Guid _entryId;
        public Entry Entry { get; private set; }

        private string _value;
        
        protected EntryData() { }

        public EntryData(Guid entryId, Guid sectionId, string value)
        {
            _entryId = entryId;
            _sectionId = sectionId;
            _value = value;
            AddEntrySubmittedDomainEvent();
        }

        public void AddEntrySubmittedDomainEvent()
        {
            AddDomainEvent(new EntryDataSubmittedDomainEvent(this, _sectionId, _value));
        }
        
        
        
    }
}
using System;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class EntryData : Entity
    {
        private Guid _elementId;

        private Guid _entryId;
        public Entry Entry { get; private set; }

        private string _value;
        
        protected EntryData() { }

        public EntryData(Guid entryId, Guid elementId, string value)
        {
            _entryId = entryId;
            _elementId = elementId;
            _value = value;
        }
    }
}
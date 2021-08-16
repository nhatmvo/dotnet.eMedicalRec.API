using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class EntryData : Entity
    {
        private Guid _elementId;

        private Guid _entryId;

        private List<string> _values;
        
        protected EntryData() { }

        public EntryData(Entry entry, Guid elementId, List<string> values)
        {
            Entry = entry;
            _elementId = elementId;
            _values = values;
        }
        
        public virtual Entry Entry { get; set; }
    }
}
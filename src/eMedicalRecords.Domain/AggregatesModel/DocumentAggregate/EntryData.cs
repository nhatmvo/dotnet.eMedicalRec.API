using System;
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
        }

        public void Validation()
        {
            
        }
    }
}
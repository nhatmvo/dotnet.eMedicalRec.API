using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Entry : Entity
    {
        private Guid _templateId;
        
        private DateTime _createdDate;
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        public string GetCreatedDateAsString => _createdDate.ToString("yy-MM-dd");

        private List<EntryData> _recordValues;

        public IReadOnlyCollection<EntryData> RecordValues => _recordValues.AsReadOnly();

        protected Entry() { }

        public Entry(Guid templateId)
        {
            _templateId = templateId;
            _createdDate = DateTime.UtcNow;
        }

    }
}
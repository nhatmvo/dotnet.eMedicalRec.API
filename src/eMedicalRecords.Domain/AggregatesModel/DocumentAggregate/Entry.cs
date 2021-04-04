using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Entry : Entity
    {
        private Guid _templateId;

        private string _name;
        private string _description;
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        private List<EntryData> _recordValues;

        public IReadOnlyCollection<EntryData> RecordValues => _recordValues.AsReadOnly();

        protected Entry() { }

        public Entry(Guid templateId, string name, string description)
        {
            _templateId = templateId;
            _name = name ?? DateTime.UtcNow.ToString("d");
            _description = description;
        }

    }
}
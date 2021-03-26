using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Entry : Entity
    {
        private Guid _headingSetId;
        public HeadingSet HeadingSet { get; private set; }

        private string _name;
        private string _description;

        private List<SectionData> _recordValues;

        public IReadOnlyCollection<SectionData> RecordValues => _recordValues.AsReadOnly();

        protected Entry() { }

        public Entry(Guid headingSetId, string name, string description)
        {
            _headingSetId = headingSetId;
            _name = name ?? DateTime.UtcNow.ToString("d");
            _description = description;
        }

        public void AddRecordAttributeDataForEntry(SectionData data)
        {
            data.Validation();
            _recordValues ??= new List<SectionData>();
            _recordValues.Add(data);
        }

    }
}
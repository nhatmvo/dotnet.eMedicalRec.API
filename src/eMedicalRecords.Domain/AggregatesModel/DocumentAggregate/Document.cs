using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Document : Entity, IAggregateRoot
    {
        private DateTime _createdDate;
        private DateTime? _updatedDate;
        
        private readonly List<Entry> _documentEntries;

        public List<Entry> DocumentEntries => _documentEntries;

        protected Document()
        {
            _documentEntries = new List<Entry>();
            _createdDate = new DateTime();
        }

        public void AddEntry(Entry entry)
        {
            _documentEntries.Add(entry);
        }
    }
}
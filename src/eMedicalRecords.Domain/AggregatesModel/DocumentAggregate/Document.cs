using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Document : Entity, IAggregateRoot
    {
        private string _name;
        private string _departmentName;
        
        private DateTime _createdDate;
        private DateTime? _updatedDate;

        private Guid _patientId;
        public Guid GetPatientId => _patientId;

        private List<Entry> _documentEntries;

        public List<Entry> DocumentEntries => _documentEntries;

        protected Document() { }

        public Document(string departmentName, Guid patientId)
        {
            _departmentName = departmentName;
            _createdDate = DateTime.UtcNow;
            _patientId = patientId;
            _documentEntries = new List<Entry>();
        }

        public void AddEntry(Entry entry)
        {
            if (_documentEntries == null)
                _documentEntries = new List<Entry>();
            _documentEntries.Add(entry);
            _updatedDate = DateTime.UtcNow;
        }

    }
}
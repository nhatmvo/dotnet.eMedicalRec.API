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

        public Guid PatientId { get; set; }
        
        private readonly List<Entry> _documentEntries;

        public List<Entry> DocumentEntries => _documentEntries;
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Document() { }

        public Document(string name, string departmentName)
        {
            _name = name;
            _departmentName = departmentName;
            _createdDate = DateTime.UtcNow;
            _documentEntries = new List<Entry>();
        }

        public void AddEntry(Entry entry)
        {
            _documentEntries.Add(entry);
            _updatedDate = DateTime.UtcNow;
        }
        
    }
}
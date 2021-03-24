using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Heading : Entity
    {
        private string _name;
        private string _description;
        
        private List<RecordAttribute> _recordAttributes;
        public IReadOnlyCollection<RecordAttribute> RecordAttributes => _recordAttributes.AsReadOnly();

        protected Heading()
        {
            _recordAttributes = new List<RecordAttribute>();
        }
        
        public Heading(string name, string description)
        {
            _name = name;
            _description = description;
        }
    }
}
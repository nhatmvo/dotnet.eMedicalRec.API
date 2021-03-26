using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Heading : Entity
    {
        private string _name;
        private string _description;
        
        private List<Section> _recordAttributes;
        public IReadOnlyCollection<Section> RecordAttributes => _recordAttributes.AsReadOnly();

        protected Heading()
        {
            _recordAttributes = new List<Section>();
        }
        
        public Heading(string name, string description)
        {
            _name = name;
            _description = description;
        }
    }
}
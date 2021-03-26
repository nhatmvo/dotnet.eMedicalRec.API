using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class SectionData : Entity
    {
        private Guid _sectionId;
        public Section Section { get; private set; }

        private Guid _entryId;
        public Entry Entry { get; private set; }
        
        private string _value;
        
        protected SectionData() { }

        public SectionData(Guid entryId, Guid sectionId, string value)
        {
            _entryId = entryId;
            _sectionId = sectionId;
            _value = value;
        }

        public void Validation()
        {
            // TODO: Check this input value is valid (data types, not null)
            var controlTypeId = Section.ControlType.Id;
            if (controlTypeId.Equals(ControlType.Checkbox.Id))
            {
                
            } else if (controlTypeId.Equals(ControlType.RadioButton.Id))
            {
                
            }
        }
    }
}
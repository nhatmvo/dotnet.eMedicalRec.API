using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class RecordAttributeData : Entity
    {
        private Guid _recordAttributeId;
        public RecordAttribute RecordAttribute { get; private set; }

        private string _value;
        
        protected RecordAttributeData() { }

        public RecordAttributeData(Guid recordAttributeId, string value)
        {
            _recordAttributeId = recordAttributeId;
            _value = value;
        }

        public void Validation()
        {
            // TODO: Check this input value is valid (data types, not null)
            var recordTypeId = RecordAttribute.ControlType.Id;
            if (recordTypeId.Equals(ControlType.Checkbox.Id))
            {
                
            } else if (recordTypeId.Equals(ControlType.RadioButton.Id))
            {
                
            }
        }
    }
}
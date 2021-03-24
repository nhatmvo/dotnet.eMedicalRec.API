using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class RecordAttribute : Entity
    {
        private Guid? _parentRecordAttributeId;
        public RecordAttribute ParentRecordAttribute { get; private set; }

        private string _name;
        private string _tooltip;
        private object _defaultValue;
        
        public ControlType ControlType { get; private set; }
        private int _controlTypeId;

        protected RecordAttribute() { }

        public RecordAttribute(string name, string tooltip, int controlTypeId, object defaultValue, Guid? parentRecordAttribute)
        {
            _name = name;
            _controlTypeId = controlTypeId;
            _tooltip = tooltip;
            _defaultValue = defaultValue;
            _parentRecordAttributeId = parentRecordAttribute;
        }
        
        
    }
}
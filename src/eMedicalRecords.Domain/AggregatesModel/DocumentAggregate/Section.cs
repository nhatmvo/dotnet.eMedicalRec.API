using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Section : Entity
    {
        private Guid? _parentSectionId;
        public Section ParentSection { get; private set; }

        private string _name;
        private string _tooltip;
        private object _defaultValue;
        
        public ControlType ControlType { get; private set; }
        private int _controlTypeId;

        protected Section() { }

        public Section(string name, string tooltip, int controlTypeId, object defaultValue, Guid? parentSectionId)
        {
            _name = name;
            _controlTypeId = controlTypeId;
            _tooltip = tooltip;
            _defaultValue = defaultValue;
            _parentSectionId = parentSectionId;
        }
        
        
    }
}
using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class Section : Entity
    {
        private Guid? _parentSectionId;
        public List<Section> ChildSections { get; private set; }

        private List<string> _options;
        private string _name;
        private string _tooltip;
        
        public ControlType ControlType { get; private set; }
        private int _controlTypeId;
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Section() { }

        public Section(string name, string tooltip, int controlTypeId, List<string> options, Guid? parentSectionId)
        {
            _name = name;
            _controlTypeId = controlTypeId;
            _tooltip = tooltip;
            _parentSectionId = parentSectionId;
            _options = options;
        }
    }
}
using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class Section : Entity
    {
        private const int ControlCheckbox = 1;
        private const int ControlRadiobutton = 2;
        private const int ControlLabel = 3;
        private const int ControlImage = 4;
        private const int ControlVideo = 5;
        private const int ControlText = 6;

        private Guid? _parentSectionId;
        public List<Section> ChildSections { get; private set; }

        private List<string> _options;
        private string _name;
        private string _tooltip;
        
        public ControlType ControlType { get; private set; }
        private int _controlTypeId;
        public override Guid Id { get; protected set; } = Guid.NewGuid();
        
        public ControlBase AdditionalControlInformation { get; set; }

        protected Section() { }

        public Section(string name, string tooltip, int controlTypeId, List<string> options, Guid? parentSectionId)
        {
            _name = name;
            _controlTypeId = controlTypeId;
            _tooltip = tooltip;
            _parentSectionId = parentSectionId;
            _options = options;
            AdditionalControlInformation = InstantiateControlById(controlTypeId);
        }

        private ControlBase InstantiateControlById(int controlTypeId)
        {
            throw new NotImplementedException();
            // switch (controlTypeId) 
            // {
            //     case ControlText:
            //         return new ControlText(-1, -1, -1, string.Empty);
            //     default:
            //         //TODO: Add Proper Domain Exception;
            //         throw new Exception();
            // }
        }
    }
}
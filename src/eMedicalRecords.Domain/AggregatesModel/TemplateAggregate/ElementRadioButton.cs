using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.Exceptions;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ElementRadioButton : ElementBase
    {
        private List<string> _options;

        private string _value;
        
        public ElementRadioButton(string name, int elementTypeId, string tooltip, string description, Guid? parentElementId, List<string> options) : base(name, elementTypeId, tooltip, description, parentElementId)
        {
            _options = options;
        }
        
        public void SetValue(string value)
        {
            _value = value;
        }

        public void ValidatePopulatedData()
        {
            if (!_options.Contains(_value))
                throw new DomainException("");

        }
    }
}
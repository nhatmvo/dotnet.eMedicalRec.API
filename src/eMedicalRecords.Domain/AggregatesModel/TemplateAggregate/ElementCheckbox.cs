using System;
using System.Collections.Generic;
using System.Linq;
using eMedicalRecords.Domain.Exceptions;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ElementCheckbox : ElementBase
    {
        private List<string> _options;
        private List<string> _values;

        public ElementCheckbox(string name, int elementTypeId, string tooltip, string description, Guid? parentElementId, List<string> options) :
            base(name, elementTypeId, tooltip, description, parentElementId)
        {
            _options = options;
        }

        public void SetValues(List<string> values)
        {
            _values = values;
        }

        public void ValidatePopulatedData()
        {
            var isSubset = !_values.Except(_options).Any();
            if (!isSubset)
                throw new DomainException("Submitted values not matching with available options");
        }
        
        
    }
}
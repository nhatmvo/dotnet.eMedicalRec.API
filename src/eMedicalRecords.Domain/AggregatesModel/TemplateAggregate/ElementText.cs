using System;
using eMedicalRecords.Domain.Exceptions;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ElementText : ElementBase
    {
        private const int NoRestriction = -1;
        
        private int? _maximumLength;
        private int? _minimumLength;
        private int? _textRestrictionLevel;
        private string _customExpression;

        private string _value;
        
        public ElementText(string name, int elementTypeId, string tooltip, string description, Guid? parentElementId) : 
            base(name, elementTypeId, tooltip, description, parentElementId)
        { }

        public void SetValue(string value)
        {
            _value = value;
        }

        public void SetValidationProperties(int? minimumLength, int? maximumLength, int? textRestrictionLevel,
            string customExpression)
        {
            _minimumLength = minimumLength ?? NoRestriction;
            _maximumLength = maximumLength ?? NoRestriction;
            _textRestrictionLevel = textRestrictionLevel ?? NoRestriction;
            _customExpression = customExpression;

        }

        public void ValidateLength()
        {
            if (_minimumLength > 0 && _value.Length < _minimumLength)
                throw new DomainException($"Value: {_value} has length that not meet minimum length requirement. (Minimum length allowed is: {_minimumLength})");
            if (_maximumLength > 0 && _value.Length > _maximumLength)
                throw new DomainException($"Value: {_value} has length that not meet maximum length requirement. (Minimum length allowed is: {_maximumLength})");
        }

        public void ValidateRestrictionLevel()
        {
            if (_textRestrictionLevel > 0)
            {
                
            }
        }

        public void ValidateCustomExpression()
        {
            if (!string.IsNullOrEmpty(_customExpression))
            {
                
            }
        }
    }
    
}
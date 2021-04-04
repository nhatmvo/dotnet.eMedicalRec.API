using System;
using eMedicalRecords.Domain.Exceptions;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ControlText : ControlBase
    {
        private const int NotRestrict = -1;
        
        private readonly int? _maximumLength;
        private readonly int? _minimumLength;
        private readonly int? _textRestrictionLevel;
        private readonly string _customExpression;

        protected ControlText()
        { }

        public ControlText(int? minimumLength, int? maximumLength, int? textRestrictionLevel, string customExpression)
        {
            _minimumLength = minimumLength ?? NotRestrict;
            _maximumLength = maximumLength ?? NotRestrict;
            _textRestrictionLevel = textRestrictionLevel ?? NotRestrict;
            _customExpression = customExpression;
        }

        public void ValidateMinimumLength(string value)
        {
            if (_minimumLength == null || _minimumLength == NotRestrict) 
                return;
            if (value.Length < _minimumLength)
                throw new DomainException(
                    $"Value: {value} has length that not meet minimum length requirement. (Minimum length allowed is: {_minimumLength})");
        }

        public void ValidateMaximumLength(string value)
        {
            if (_maximumLength == null || _maximumLength == NotRestrict)
                return;
            if (value.Length > _maximumLength)
                throw new DomainException(
                    $"Value: {value} has length that not meet maximum length requirement. (Minimum length allowed is: {_maximumLength})");
                    
        }

        public void ValidateTextRestrictionLevel(string value)
        {
            
        }

        public void ValidateWithCustomExpression(string value)
        {
            
        }

    }
}
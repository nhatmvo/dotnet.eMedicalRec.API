using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class Template : Entity, IAggregateRoot
    {
        private bool _isDefault;
        private string _name;
        private string _description;
        private bool _isDirty;

        private List<ElementBase> _elements;
        public IReadOnlyCollection<ElementBase> Elements => _elements.AsReadOnly();
        public sealed override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Template()
        { }

        public Template(string name, string description, List<ElementBase> elements)
        {
            _name = name;
            _isDirty = true;
            _description = description;
            _elements ??= new List<ElementBase>();
            _elements.AddRange(elements);
        }

        public void WrapUp()
        {
            AddDomainEvent(new TemplateAddedDomainEvent(this, Id.ToString()));
        }

        public void RemoveUpperLevelStructure()
        {
            _elements.RemoveAll(e => e.GetParentElementId() != null);
        }
    }
}
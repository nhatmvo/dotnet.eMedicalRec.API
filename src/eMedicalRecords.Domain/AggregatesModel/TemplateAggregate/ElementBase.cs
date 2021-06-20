using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public abstract class ElementBase : Entity
    {
        private Guid _templateId;

        private Guid? _parentElementId;
        public List<ElementBase> ChildElements { get; private set; }
        
        private string _tooltip;
        private string _name;
        private string _description;
        
        public ElementType ElementType { get; set; }
        private int _elementTypeId;

        protected ElementBase(string name, int elementTypeId, string tooltip, string description, Guid? parentElementId)
        {
            base.Id = Guid.NewGuid();
            _parentElementId = parentElementId;
            _tooltip = tooltip;
            _name = name;
            _elementTypeId = elementTypeId;
            _description = description;
        }
    }
}
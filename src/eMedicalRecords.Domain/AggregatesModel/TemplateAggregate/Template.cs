using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class Template : Entity, IAggregateRoot
    {
        private bool _isDefault;
        private string _name;
        private string _description;

        private readonly List<Section> _sections;
        public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Template()
        { }

        public Template(string name, string description)
        {
            _name = name;
            _description = description;
            _sections = new List<Section>();
        }

        public void AddSections(List<Section> section)
        {
            _sections.AddRange(section);
        }
    }
}
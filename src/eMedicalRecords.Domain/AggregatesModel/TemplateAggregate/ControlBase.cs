using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public abstract class ControlBase : Entity
    {
        private string _name;

        public Guid SectionId { get; set; }
        public Section Section { get; private set; }
    }
}
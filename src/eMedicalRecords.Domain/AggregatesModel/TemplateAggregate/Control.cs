using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class Control : Entity
    {
        private string _name;

        private Guid _recordAttributeId;
        public Section Section { get; private set; }
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        public Control(Guid recordAttributeId, string name)
        {
            _recordAttributeId = recordAttributeId;
            _name = name;
        }
    }
}
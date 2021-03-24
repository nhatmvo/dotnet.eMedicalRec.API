using System;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class Control : Entity
    {
        private string _name;

        private Guid _recordAttributeId;
        public RecordAttribute RecordAttribute { get; private set; }

        public Control(Guid recordAttributeId, string name)
        {
            _recordAttributeId = recordAttributeId;
            _name = name;
        }
    }
}
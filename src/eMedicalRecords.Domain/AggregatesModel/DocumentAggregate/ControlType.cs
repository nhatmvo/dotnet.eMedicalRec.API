using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public class ControlType : Enumeration
    {
        public static ControlType Checkbox = new ControlType(1, nameof(Checkbox));
        public static ControlType RadioButton = new ControlType(2, nameof(RadioButton));
        public static ControlType Label = new ControlType(3, nameof(Label));
        
        public ControlType(int id, string name) : base(id, name)
        {
        }
    }
}
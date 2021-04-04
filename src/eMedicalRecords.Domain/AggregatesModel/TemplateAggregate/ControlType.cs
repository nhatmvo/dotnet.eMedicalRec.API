using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ControlType : Enumeration
    {
        public static ControlType Checkbox = new ControlType(1, nameof(Checkbox));
        public static ControlType RadioButton = new ControlType(2, nameof(RadioButton));
        public static ControlType Label = new ControlType(3, nameof(Label));
        public static ControlType Image = new ControlType(4, nameof(Image));
        public static ControlType Video = new ControlType(5, nameof(Video));
        public static ControlType Text = new ControlType(6, nameof(Text));
        
        public ControlType(int id, string name) : base(id, name)
        {
        }
    }
}
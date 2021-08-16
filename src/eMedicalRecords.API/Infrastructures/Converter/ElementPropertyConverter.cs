using System;
using eMedicalRecords.API.Applications.Commands.Template;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Newtonsoft.Json.Linq;

namespace eMedicalRecords.API.Infrastructures.Converter
{
    public class ElementPropertyConverter : JsonCreationConverter<ElementProperty>
    {
        protected override ElementProperty Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException();
            var typeId = (int?)jObject["elementTypeId"];
            switch (typeId)
            {
                case (int) ElementTypeEnum.Checkbox:
                    return new CheckboxProperty();
                case (int) ElementTypeEnum.Text:
                case (int) ElementTypeEnum.TextArea:
                    return new TextProperty();
                case (int) ElementTypeEnum.RadioButton:
                    return new RadiobuttonProperty();
                default:
                    return new LabelProperty();
            }
        }
    }
}
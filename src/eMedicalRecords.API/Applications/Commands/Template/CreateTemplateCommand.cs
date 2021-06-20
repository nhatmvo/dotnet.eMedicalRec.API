using System.Collections.Generic;
using System.Runtime.Serialization;
using eMedicalRecords.API.Infrastructures.Converter;
using MediatR;
using Newtonsoft.Json;

namespace eMedicalRecords.API.Applications.Commands.Template
{
    public class CreateTemplateCommand : IRequest<string>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public bool IsDefault { get; set; }
        [DataMember] public List<ElementProperty> Elements { get; set; }

    }

    [JsonConverter(typeof(ElementPropertyConverter))]
    public abstract class ElementProperty
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string Tooltip { get; set; }
        [DataMember] public int ElementTypeId { get; set; }
        [DataMember] public List<ElementProperty> ChildElements { get; set; }
    }
    

    public class TextProperty : ElementProperty
    {
        [DataMember] public int MaximumLength { get; set; }
        [DataMember] public int MinimumLength { get; set; }
        [DataMember] public int TextRestrictionLevel { get; set; }
        [DataMember] public string CustomExpression { get; set; }
    }

    public class CheckboxProperty : ElementProperty
    {
        [DataMember] public List<string> Values { get; set; }
    }

    public class RadiobuttonProperty : ElementProperty
    {
        [DataMember] public List<string> Values { get; set; }
    }

    public class ImageProperty : ElementProperty { }
    public class LabelProperty : ElementProperty { }
    public class VideoProperty : ElementProperty { }
}
using System;
using System.Collections.Generic;
using System.Linq;
using eMedicalRecords.Domain.Exceptions;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ElementType : Enumeration
    {
        public static ElementType BlurSection = new ElementType(1, nameof(BlurSection));
        public static ElementType BoldHeader = new ElementType(2, nameof(BoldHeader));
        public static ElementType TextArea = new ElementType(3, nameof(TextArea));
        public static ElementType Text = new ElementType(4, nameof(Text));
        public static ElementType Image = new ElementType(5, nameof(Image));
        public static ElementType Video = new ElementType(6, nameof(Video));
        public static ElementType Checkbox = new ElementType(7, nameof(Checkbox));
        public static ElementType RadioButton = new ElementType(8, nameof(RadioButton));
        
        public ElementType(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<ElementType> List() => new[] {BlurSection, BoldHeader, TextArea, TextArea, Text, Image, Video, Checkbox, RadioButton };

        public static ElementType FromName(string name)
        {
            var type = List().SingleOrDefault(t => String.Equals(t.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (type == null)
                throw new DomainException(
                    $"$Possible values for Element Type: {String.Join(",", List().Select(t => t.Name))}");

            return type;
        }

        public static ElementType From(int id)
        {
            var type = List().SingleOrDefault(t => t.Id == id);
            if (type == null)
                throw new DomainException(
                    $"$Possible values for Element Type: {String.Join(",", List().Select(t => t.Name))}");

            return type;
        }
    }

    public enum ElementTypeEnum
    {
        BlurSection = 1,
        BoldHeader = 2,
        TextArea = 3,
        Text = 4,
        Image = 5,
        Video = 6,
        Checkbox = 7,
        RadioButton = 8
    }
}
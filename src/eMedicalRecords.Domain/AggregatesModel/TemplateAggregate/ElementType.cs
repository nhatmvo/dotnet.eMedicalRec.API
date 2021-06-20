using System;
using System.Collections.Generic;
using System.Linq;
using eMedicalRecords.Domain.Exceptions;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public class ElementType : Enumeration
    {
        public static ElementType Checkbox = new ElementType(1, nameof(Checkbox));
        public static ElementType RadioButton = new ElementType(2, nameof(RadioButton));
        public static ElementType Label = new ElementType(3, nameof(Label));
        public static ElementType Image = new ElementType(4, nameof(Image));
        public static ElementType Video = new ElementType(5, nameof(Video));
        public static ElementType Text = new ElementType(6, nameof(Text));
        
        public ElementType(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<ElementType> List() => new[] {Checkbox, RadioButton, Label, Image, Video, Text};

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
        Checkbox = 1,
        RadioButton = 2,
        Label = 3,
        Image = 4,
        Video = 5,
        Text = 6
    }
}
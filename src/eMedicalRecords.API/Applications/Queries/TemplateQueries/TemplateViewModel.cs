using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
// ReSharper disable InconsistentNaming

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    [BsonIgnoreExtraElements]
    public record TemplateDTO
    {
        public Guid _id { get; init; }
        public string name { get; init; }
        public string description { get; init; }
        public bool isDefault { get; set; }
        public bool isDirty { get; set; }
        public List<ElementBase> elements { get; set; }

        public static TemplateDTO FromTemplateBson(BsonDocument document)
        {
            return BsonSerializer.Deserialize<TemplateDTO>(document);
        }
    }
    
    [BsonIgnoreExtraElements]
    public record ElementBase
    {
        public Guid Id { get; init; }
        public string name { get; init; }
        public string description { get; init; }
        public int elementTypeId { get; init; }
        public Guid? parentElementId { get; set; }
        public List<ElementBase> ChildElements { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record ElementText : ElementBase
    {
        public int maximumLength { get; set; }
        public int minimumLength { get; set; }
        public int textRestrictionLevel { get; set; }
        public string customExpression { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record ElementCheckbox : ElementBase
    {
        public List<string> options { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record ElementRadioButton : ElementBase
    {
        public List<string> options { get; set; }
    }
}
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Infrastructure.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace eMedicalRecords.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IMongoCollection<Template> _templates;
        private readonly IMongoCollection<BsonDocument> _bsonTemplates;

        public TemplateService(ITemplateDatabaseSettings settings)
        {
            RegisterBackingFieldMapper();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _templates = database.GetCollection<Template>(settings.TemplatesCollectionName);
            _bsonTemplates = database.GetCollection<BsonDocument>(settings.TemplatesCollectionName);
        }
        
        public Task AddTemplateAsync(Template template)
        {
            return _templates.InsertOneAsync(template);
        }

        public Task UpdateTemplateAsync(Guid templateId, Template templateToUpdate)
        {
             return _templates.ReplaceOneAsync(b => b.Id == templateId, templateToUpdate);
        }

        public Task DeleteTemplateAsync(Guid templateId)
        {
            return _templates.DeleteOneAsync(b => b.Id == templateId);
        }

        public async Task<BsonDocument> GetTemplateAsync(Guid templateId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", templateId);
            var results = await _bsonTemplates.Find(filter).ToListAsync();
            if (results.Count() > 1)
                throw new DataException($"There are more than one template found for id: {templateId}");

            return results.FirstOrDefault();
        }

        private void RegisterBackingFieldMapper()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            BsonClassMap.RegisterClassMap<Template>(map =>
            {
                // map.AutoMap();
                map.MapField("_isDefault").SetElementName("isDefault");
                map.MapField("_name").SetElementName("name");
                map.MapField("_description").SetElementName("description");
                map.MapField("_isDirty").SetElementName("isDirty");
                map.MapField("_elements").SetElementName("elements");
                // map.MapField("_id").SetElementName("id");
            });

            BsonClassMap.RegisterClassMap<ElementBase>(map =>
            {
                map.AutoMap();
                map.MapField("_parentElementId").SetElementName("parentElementId");
                map.MapField("_tooltip").SetElementName("tooltip");
                map.MapField("_name").SetElementName("name");
                map.MapField("_description").SetElementName("description");
                map.MapField("_elementTypeId").SetElementName("elementTypeId");
            });

            BsonClassMap.RegisterClassMap<ElementText>(map =>
            {
                map.AutoMap();
                map.MapField("_maximumLength").SetElementName("maximumLength");
                map.MapField("_minimumLength").SetElementName("minimumLength");
                map.MapField("_textRestrictionLevel").SetElementName("textRestrictionLevel");
                map.MapField("_customExpression").SetElementName("customExpression");
            });

            BsonClassMap.RegisterClassMap<ElementCheckbox>(map =>
            {
                map.AutoMap();
                map.MapField("_options").SetElementName("options");
                map.MapField("_values").SetElementName("values");
            });

            BsonClassMap.RegisterClassMap<ElementRadioButton>(map =>
            {
                map.AutoMap();
                map.MapField("_options").SetElementName("options");
                map.MapField("_value").SetElementName("value");
            });
        }
    }
}
using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Infrastructure.Configurations;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace eMedicalRecords.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IMongoCollection<Template> _templates;

        public TemplateService(ITemplateDatabaseSettings settings)
        {
            RegisterBackingFieldMapper();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _templates = database.GetCollection<Template>(settings.TemplatesCollectionName);
        }
        
        public Task AddTemplateFromReadDb(Template template)
        {
            return _templates.InsertOneAsync(template);
        }

        public Task UpdateTemplateFromReadDb(Guid templateId, Template templateToUpdate)
        {
             return _templates.ReplaceOneAsync(b => b.Id == templateId, templateToUpdate);
        }

        public Task DeleteTemplateFromReadDb(Guid templateId)
        {
            return _templates.DeleteOneAsync(b => b.Id == templateId);
        }

        private void RegisterBackingFieldMapper()
        {
            BsonClassMap.RegisterClassMap<Template>(map =>
            {
                map.AutoMap();
                map.MapField("_isDefault").SetElementName("isDefault");
                map.MapField("_name").SetElementName("name");
                map.MapField("_description").SetElementName("description");
                map.MapField("_isDirty").SetElementName("isDirty");
                map.MapField(f => f.Elements).SetElementName("elements");
            });

            BsonClassMap.RegisterClassMap<ElementBase>(map =>
            {
                map.AutoMap();
                map.MapField("_parentElementId").SetElementName("parentElementId");
                map.MapField("_tooltip").SetElementName("tooltip");
                map.MapField("_name").SetElementName("name");
                map.MapField("_description").SetElementName("description");
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
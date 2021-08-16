using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateService
    {
        Task AddTemplateAsync(Template template);

        Task UpdateTemplateAsync(Guid templateId, Template templateToUpdate);

        Task DeleteTemplateAsync(Guid templateId);

        Task<BsonDocument> GetTemplateAsync(Guid templateId);
    }
}
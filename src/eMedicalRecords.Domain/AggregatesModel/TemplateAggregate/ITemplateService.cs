using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateService
    {
        Task AddTemplateFromReadDb(Template template);

        Task UpdateTemplateFromReadDb(Guid templateId, Template templateToUpdate);

        Task DeleteTemplateFromReadDb(Guid templateId);
    }
}
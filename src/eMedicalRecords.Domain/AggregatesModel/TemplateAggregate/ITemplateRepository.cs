using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<Template> AddTemplate(Template headingSet);
        Task<ElementBase> GetElementValidationTypeById(Guid elementId);
        Task DeleteTemplateById(Guid templateId);

        IAsyncEnumerable<Template> GetDirtyTemplate();

        Task<Template> GetTemplateById(Guid templateId);
    }
}
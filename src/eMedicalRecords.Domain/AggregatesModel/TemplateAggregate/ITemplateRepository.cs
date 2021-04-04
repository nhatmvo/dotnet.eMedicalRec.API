using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<Template> AddTemplate(Template headingSet);

        Task<List<string>> GetAvailableSectionOptions(Guid sectionId);
        Task<ControlBase> GetControlTypeBySectionId(Guid sectionId);
    }
}
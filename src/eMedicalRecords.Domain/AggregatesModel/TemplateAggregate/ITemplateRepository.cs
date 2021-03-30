using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<Template> AddTemplate(Template headingSet);
    }
}
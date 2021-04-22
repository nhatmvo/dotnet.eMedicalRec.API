using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.TemplateAggregate
{
    public interface ITemplateRedisRepository : IRepository<Template>
    {
        Task<bool> UpsertTemplateAsync(string templateId, string templateStructure);
        Task<string> GetTemplateAsync(string id);
    }
}
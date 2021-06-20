using System;
using System.Threading.Tasks;

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    public interface ITemplateQueries
    {
        public Task<Template> GetTemplateAsync(Guid id);
    }
}
using System;
using System.Threading.Tasks;

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    public class TemplateQueries : ITemplateQueries
    {
        public Task<Template> GetTemplateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
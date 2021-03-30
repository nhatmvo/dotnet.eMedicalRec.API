using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly MedicalRecordContext _context;

        public TemplateRepository(MedicalRecordContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        
        public async Task<Template> AddTemplate(Template template)
        {
            var entry = await _context.Templates.AddAsync(template);
            return entry.Entity;
        }
    }
}
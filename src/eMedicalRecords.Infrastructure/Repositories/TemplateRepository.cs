using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;

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

        public Task<List<string>> GetAvailableSectionOptions(Guid sectionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ControlBase> GetControlTypeBySectionId(Guid sectionId)
        {
            var text = await _context.Controls.OfType<ControlText>()
                .FirstOrDefaultAsync(p => p.SectionId == sectionId);
            if (text != null)
                return text;

            throw new NotImplementedException();

        }
    }
}
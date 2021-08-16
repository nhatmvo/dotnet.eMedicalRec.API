using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.SeedWorks;
using eMedicalRecords.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

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

        public Task<ElementBase> GetElementValidationTypeById(Guid elementId)
        {
            return _context.ElementBases
                .Include(eb => eb.ElementType)
                .FirstOrDefaultAsync(eb => eb.Id.Equals(elementId));
        }

        public async Task DeleteTemplateById(Guid templateId)
        {
            var templateToDelete = await _context.Templates.FindAsync(templateId);
            if (templateToDelete != null)
                _context.Templates.Remove(templateToDelete);
        }

        public IAsyncEnumerable<Template> GetDirtyTemplate()
        {
            return _context.Templates.Where(t => EF.Property<bool>(t, "_isDirty")).AsAsyncEnumerable();
        }

        public async Task<Template> GetTemplateById(Guid templateId)
        {
            var template = await _context.Templates
                .Include(t => t.Elements)
                .ThenInclude(e => e.ChildElements)
                .FirstOrDefaultAsync(t => t.Id.Equals(templateId));

            if (template == null)
                throw new RecordNotFoundException($"Cannot find template with id: ${templateId}");
            return template;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace eMedicalRecords.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MedicalRecordContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public DocumentRepository(MedicalRecordContext context)
        {
            _context = context;
        }

        public async Task<Document> Add(Document document)
        {
            var entry = await _context.Documents.AddAsync(document);
            return entry.Entity;
        }

        public async Task<Document> FindById(Guid documentId)
        {
            var documentToFind = await _context.Documents
                .FirstOrDefaultAsync(d => d.Id == documentId);
            return documentToFind;
        }

        public async Task<List<Document>> FindDocumentsByPatientId(Guid patientId)
        {
            var documentsToFind = await _context.Documents
                .Where(d => EF.Property<Guid>(d, "_patientId") == patientId)
                .ToListAsync();
            return documentsToFind;
        }

        public Document Update(Document document)
        {
            return _context.Documents
                .Update(document)
                .Entity;
        }

        public async Task SubmitEntryData(IEnumerable<EntryData> data)
        {
            await _context.EntryData.AddRangeAsync(data);
        }
    }
}
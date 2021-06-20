using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.DocumentAggregate
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<Document> Add(Document document);
        Task<Document> FindById(Guid documentId);
        Task<List<Document>> FindDocumentsByPatientId(Guid patientId);
        Task SubmitEntryData(IEnumerable<EntryData> data);
        Document Update(Document document);
    }
}
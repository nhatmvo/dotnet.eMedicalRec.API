using System;
using System.Threading.Tasks;

namespace eMedicalRecords.API.Applications.Queries.DocumentQueries
{
    public interface IDocumentQueries
    {
        Task<DocumentEntryData> GetEntryData(Guid entryId);
    }
}
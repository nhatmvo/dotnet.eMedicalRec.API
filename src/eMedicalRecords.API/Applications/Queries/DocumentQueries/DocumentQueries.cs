using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using eMedicalRecords.Infrastructure.Exceptions;
using Npgsql;

namespace eMedicalRecords.API.Applications.Queries.DocumentQueries
{
    public class DocumentQueries : IDocumentQueries
    {
        private readonly string _connectionString;

        public DocumentQueries(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<DocumentEntryData> GetEntryData(Guid entryId)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("entryId", entryId);

            var result = await connection.QueryAsync<dynamic>(@"
                SELECT de.id AS entryid, de.created_date as entrycreateddate, de.document_id as documentid, ded.element_id AS elementid, ded.values AS values
                    FROM document_entry de INNER JOIN document_entry_data ded ON de.id = ded.entry_id
                WHERE de.id = @entryId", dynamicParams);
            
            var currentDocumentEntry = result.ToList().Count > 0 ? MapFromQuery(result) : throw new RecordNotFoundException();
            var currentDocId = currentDocumentEntry.DocumentId;
            var currentEntryDate = currentDocumentEntry.EntryDate;

            currentDocumentEntry.PriorDocumentEntry =
                await GetPriorDocumentEntry(connection, currentDocId, currentEntryDate);

            currentDocumentEntry.SucceedingDocumentEntry =
                await GetSucceedingDocumentEntry(connection, currentDocId, currentEntryDate);
            
            return currentDocumentEntry;
        }

        private async Task<EntrySummary> GetPriorDocumentEntry(NpgsqlConnection connection, Guid? documentId, DateTime currentEntryDate)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("documentId", documentId);
            dynamicParams.Add("entryDate", currentEntryDate);
            var result = await connection.QueryAsync<EntrySummary>(@"SELECT de.id as entryid, de.created_date AS entrydate FROM document_entry de
                WHERE de.document_id = @documentId AND de.created_date < @entryDate FETCH FIRST 1 ROWS ONLY", dynamicParams);

            return result.FirstOrDefault();
        }
        
        private async Task<EntrySummary> GetSucceedingDocumentEntry(NpgsqlConnection connection, Guid? documentId, DateTime currentEntryDate)
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("documentId", documentId);
            dynamicParams.Add("entryDate", currentEntryDate);
            var result = await connection.QueryAsync<EntrySummary>(@"SELECT de.id as entryid, de.created_date AS entrydate FROM document_entry de
                WHERE de.document_id = @documentId AND de.created_date > @entryDate FETCH FIRST 1 ROWS ONLY", dynamicParams);

            return result.FirstOrDefault();
        }

        private DocumentEntryData MapFromQuery(dynamic result)
        {
            var documentEntryData = new DocumentEntryData
            {
                EntryId = result[0].entryid,
                EntryDate = result[0].entrycreateddate,
                DocumentId = result[0].documentid,
                ElementValues = new List<DataValue>()
            };
            foreach (var row in result)
            {
                if (row.elementid == null) break;
                documentEntryData.ElementValues.Add(new DataValue
                {
                    ElementId = row.elementid,
                    Values = row.values
                });
            }
            return documentEntryData;
        }
    }
}
using System;
using System.Collections.Generic;

namespace eMedicalRecords.API.Applications.Queries.DocumentQueries
{
    public record DocumentEntryData
    {
        public Guid EntryId { get; init; }
        public DateTime EntryDate { get; init; }
        public Guid? DocumentId { get; init; }
        public EntrySummary PriorDocumentEntry { get; set; }
        public EntrySummary SucceedingDocumentEntry { get; set; }
        public List<DataValue> ElementValues { get; init; }
    }

    public record DataValue
    {
        public Guid ElementId { get; init; }
        public string[] Values { get; init; }
    }

    public record EntrySummary
    {
        public Guid EntryId { get; init; }
        public DateTime EntryDate { get; init; }
    }
}
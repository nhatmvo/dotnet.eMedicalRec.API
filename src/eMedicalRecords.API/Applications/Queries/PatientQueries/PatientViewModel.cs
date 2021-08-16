using System;
using System.Collections.Generic;

namespace eMedicalRecords.API.Applications.Queries.PatientQueries
{
    public record PatientView
    {
        public Guid PatientId { get; init; }
        public string Name { get; init; }
        public DateTime DayOfBirth { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime? LatestExaminationDate { get; init; }
        public long NumberOfEntries { get; init; }
        public Guid DocumentId { get; init; }
        public string Gender { get; init; }
    }

    public record PatientDetails
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string PhoneNumber { get; init; }
        public string Gender { get; init; }
        public List<PatientEntry> PatientEntries { get; init; }
    }

    public record PatientEntry
    {
        public DateTime EntryDate { get; init; }
        public string MainEntryName { get; init; }
        public string[] MainEntryValue { get; init; }
        public Guid EntryId { get; init; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;

namespace eMedicalRecords.API.Applications.Queries.PatientQueries
{
    public interface IPatientQueries
    {
        Task<PatientDetails> GetPatientAsync(Guid patientId, Guid selectedElementId);

        Task<IEnumerable<PatientView>> GetPatientsAsync(string filter);
    }
}
using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.PatientAggregate
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> FindPatientByIdentityNo(string identityNo, int identityTypeId);
        Task<Patient> FindPatientByPatientNo(string patientNo);
        Task<Patient> AddAsync(Patient patient);
        Patient Update(Patient patient);
    }
}
using System.Threading.Tasks;

namespace eMedicalRecords.API.Applications.Queries.PatientQueries
{
    public interface IPatientQueries
    {
        Task<Patient> GetPatientAsync(string identityNo, int identityTypeId);
    }
}
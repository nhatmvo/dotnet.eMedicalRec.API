using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using eMedicalRecords.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace eMedicalRecords.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicalRecordContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public PatientRepository(MedicalRecordContext context)
        {
            _context = context;
        }
        
        public Task<Patient> FindPatientByIdentityNo(string identityNo, int identityTypeId)
        {
            return _context.Patients.FirstOrDefaultAsync(p =>
                EF.Property<string>(p, "_identityNo") == identityNo && p.IdentityType.Id == identityTypeId);
        }

        public Task<Patient> FindPatientByPatientNo(string patientNo)
        {
            throw new System.NotImplementedException();
        }

        public Task<Patient> FindPatientById(Guid id)
        {
            return _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            var entity = await _context.Patients.AddAsync(patient);
            return entity.Entity;
        }

        public Patient Update(Patient patient)
        {
            throw new System.NotImplementedException();
        }
    }
}
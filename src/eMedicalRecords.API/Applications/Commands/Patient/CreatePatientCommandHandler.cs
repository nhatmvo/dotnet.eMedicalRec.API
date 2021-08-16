using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Patient
{
    using Domain.AggregatesModel.PatientAggregate;
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<CreatePatientCommandHandler> _logger;

        public CreatePatientCommandHandler(IPatientRepository patientRepository,
            ILogger<CreatePatientCommandHandler> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }
        
        public async Task<PatientDTO> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientIdentifier = request.HospitalDocumentId;
            var patientToAdd = await _patientRepository.FindPatientByIdentityNo(request.IdentityNo, request.IdentityTypeId);
            if (patientToAdd != null)
            {
                _logger.LogError(
                    "This patient with identity: {IdentityNo} of type {IdentityType} already been registered with the system",
                    request.IdentityNo, request.IdentityTypeId);
                return null;
            }

            var gender = request.Gender == "male";
            var address = new PatientAddress(request.Country, request.City, request.District, request.AddressLine);
            var patient = new Patient(patientIdentifier, request.IdentityNo, request.IdentityTypeId, request.Name, gender, request.Email, request.PhoneNumber, request.DateOfBirth, address,
                request.HasInsurance, request.AdmissionDate, request.ExaminationDate, request.SurgeryDate);

            var addedPatient = await _patientRepository.AddAsync(patient);
            await _patientRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            
            return PatientDTO.FromPatient(addedPatient);
        }
    }

    public record PatientDTO
    {
        public string IdentityNo { get; init; }

        public static PatientDTO FromPatient(Patient patient)
        {
            return new PatientDTO
            {
                IdentityNo = patient.Id.ToString("N")
            };

        } 
    }
    
}
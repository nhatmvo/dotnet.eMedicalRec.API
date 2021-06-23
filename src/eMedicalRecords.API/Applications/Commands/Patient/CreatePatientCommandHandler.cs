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
            var patientIdentifier = await GetPatientIdentifierNumber();
            var patientToAdd = await _patientRepository.FindPatientByIdentityNo(request.IdentityNo, request.IdentityTypeId);
            if (patientToAdd != null)
            {
                _logger.LogError(
                    "This patient with identity: {IdentityNo} of type {IdentityType} already been registered with the system",
                    request.IdentityNo, request.IdentityTypeId);
                return null;
            }
            
            var address = new PatientAddress(request.Country, request.City, request.District, request.AddressLine);
            var patient = new Patient(patientIdentifier, request.IdentityNo, request.IdentityTypeId, request.FirstName, request.LastName,
                request.MiddleName, request.Email, request.PhoneNumber, request.DateOfBirth, address,
                request.HasInsurance, request.Description);

            await _patientRepository.AddAsync(patient);
            await _patientRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            
            return PatientDTO.FromPatient(patient);
        }

        private async Task<string> GetPatientIdentifierNumber()
        {
            while (true)
            {
                var potentialIdentifier = new Random().Next(100000000).ToString("D8");
                var existingPatient = await _patientRepository.FindPatientByPatientNo(potentialIdentifier);
                if (existingPatient == null)
                    return potentialIdentifier;
            }
        }
    }

    public record PatientDTO
    {
        public string IdentityNo { get; init; }
        public int IdentityTypeId { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string District { get; init; }
        public string AddressLine { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public DateTime DateOfBirth { get; init; }
        public bool HasInsurance { get; init; }
        public string Description { get; init; }

        public static PatientDTO FromPatient(Patient patient)
        {
            return new PatientDTO();

        } 
    }
    
}
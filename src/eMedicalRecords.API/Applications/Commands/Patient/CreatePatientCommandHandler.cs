using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Patient
{
    using Domain.AggregatesModel.PatientAggregate;
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<CreatePatientCommandHandler> _logger;

        public CreatePatientCommandHandler(IPatientRepository patientRepository,
            ILogger<CreatePatientCommandHandler> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }
        
        public async Task<bool> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientIdentifier = await GetPatientIdentifierNumber();
            var patientToAdd = await _patientRepository.FindPatientByIdentityNo(request.IdentityNo, request.IdentityTypeId);
            if (patientToAdd != null)
            {
                _logger.LogError(
                    "This patient with identity: {IdentityNo} of type {IdentityType} already been registered with the system",
                    request.IdentityNo, request.IdentityTypeId);
                return false;
            }
            
            var address = new PatientAddress(request.Country, request.City, request.District, request.AddressLine);
            var patient = new Patient(patientIdentifier, request.IdentityNo, request.IdentityTypeId, request.FirstName, request.LastName,
                request.MiddleName, request.Email, request.PhoneNumber, request.DateOfBirth, address,
                request.HasInsurance, request.Description);

            await _patientRepository.AddAsync(patient);
            await _patientRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            return true;
        }

        private async Task<string> GetPatientIdentifierNumber()
        {
            while (true)
            {
                var potentialIdentifier = new Random().Next(100000000).ToString("D8");
                var existingPatient = await _patientRepository.FindByPatientNo(potentialIdentifier);
                if (existingPatient == null)
                    return potentialIdentifier;
            }
        }
    }
}
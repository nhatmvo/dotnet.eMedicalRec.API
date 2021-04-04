using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using eMedicalRecords.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Patient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<UpdatePatientCommandHandler> _logger;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository,
            ILogger<UpdatePatientCommandHandler> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }
        
        public async Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientToUpdate = await _patientRepository.FindPatientByPatientNo(request.PatientNo);
            patientToUpdate.UpdatePatientInformation();
            
            _logger.LogInformation("----- Updating patient's information - Patient: {@PatientNo}", patientToUpdate.PatientNo);
            _patientRepository.Update(patientToUpdate);
            
            return await _patientRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            
        }
    }
    
    public class UpdatePatientIdentifiedCommandHandler : IdentifiedCommandHandler<UpdatePatientCommand, bool>
    {
        public UpdatePatientIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            ILogger<UpdatePatientIdentifiedCommandHandler> logger) : base(mediator, requestManager,
            logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}
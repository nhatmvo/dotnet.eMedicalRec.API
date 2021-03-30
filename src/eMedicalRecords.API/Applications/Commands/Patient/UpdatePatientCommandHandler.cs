using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Patient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        
        public async Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientToUpdate = await _patientRepository.FindByPatientNo(request.PatientNo);
            return true;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    using Domain.AggregatesModel.DocumentAggregate;
    
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<CreateDocumentCommandHandler> _logger;

        public CreateDocumentCommandHandler(IDocumentRepository documentRepository,
            IPatientRepository patientRepository,
            ILogger<CreateDocumentCommandHandler> logger)
        {
            _documentRepository = documentRepository;
            _patientRepository = patientRepository;
            _logger = logger;
        }
        
        public async Task<bool> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.FindPatientByPatientNo(request.PatientNo);
            if (patient == null)
                throw new Exception();

            var documentToCreate = new Document(request.Name, request.DepartmentName, patient.Id);
            _logger.LogInformation("----- Creating document for patient {PatientNo}", patient.PatientNo);
            await _documentRepository.Add(documentToCreate);
            
            return await _documentRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
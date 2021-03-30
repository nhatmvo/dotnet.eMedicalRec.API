using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    using Domain.AggregatesModel.DocumentAggregate;
    
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<CreateDocumentCommandHandler> _logger;

        public CreateDocumentCommandHandler(IDocumentRepository documentRepository,
            ILogger<CreateDocumentCommandHandler> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }
        
        public async Task<bool> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentToCreate = new Document(request.Name, request.DepartmentName);
            documentToCreate.PatientId = Guid.Parse(request.PatientId);
            
            await _documentRepository.Add(documentToCreate);
            await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
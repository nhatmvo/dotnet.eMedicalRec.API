using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class AddEntryCommandHandler : IRequestHandler<AddEntryCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public AddEntryCommandHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        
        public async Task<bool> Handle(AddEntryCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.FindById(request.DocumentId);
            document.AddEntry(new Entry(request.TemplateId, request.Name, request.Description));
            _documentRepository.Update(document);
            
            await _documentRepository
                .UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
    
    public class AddEntryIdentifiedCommandHandler : IdentifiedCommandHandler<AddEntryCommand, bool>
    {
        public AddEntryIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            ILogger<AddEntryIdentifiedCommandHandler> logger) : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    using Domain.AggregatesModel.DocumentAggregate;
    
    public class SubmitDataCommandHandler : IRequestHandler<SubmitDataCommand, bool>
    {
        private readonly ILogger<SubmitDataCommandHandler> _logger;
        private readonly IDocumentRepository _documentRepository;

        public SubmitDataCommandHandler(IDocumentRepository documentRepository,
            ILogger<SubmitDataCommandHandler> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }
        
        public async Task<bool> Handle(SubmitDataCommand request, CancellationToken cancellationToken)
        {
            var dataBulk = request.EntryDataRequests
                    .Select(edr => new EntryData(edr.EntryId, edr.SectionId, edr.Value))
                    .ToList();

            await _documentRepository.SubmitEntryData(dataBulk);
            await _documentRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
    
    public class SubmitDataIdentifiedCommandHandler : IdentifiedCommandHandler<SubmitDataCommand, bool>
    {
        public SubmitDataIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            ILogger<SubmitDataIdentifiedCommandHandler> logger) : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}
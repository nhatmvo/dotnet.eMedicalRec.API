using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class SubmitDataCommandHandler : IRequestHandler<SubmitDataCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public SubmitDataCommandHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        
        public async Task<bool> Handle(SubmitDataCommand request, CancellationToken cancellationToken)
        {
            var entryData = request.EntryDataRequests
                    .Select(edr => new EntryData(edr.EntryId, edr.SectionId, edr.Value));
            // TODO: Validation when data submission.
            await _documentRepository.SubmitEntryData(entryData);
            await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
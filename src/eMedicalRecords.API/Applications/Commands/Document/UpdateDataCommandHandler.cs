using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class UpdateDataCommandHandler : IRequestHandler<UpdateDataCommand, bool>
    {
        public Task<bool> Handle(UpdateDataCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
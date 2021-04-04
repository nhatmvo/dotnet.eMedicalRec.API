using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.API.Applications.Commands.Document;
using eMedicalRecords.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands
{
    public class IdentifiedCommandHandler<T, TR> : IRequestHandler<IdentifiedCommand<T, TR>, TR>
        where T : IRequest<TR>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly ILogger<IdentifiedCommandHandler<T, TR>> _logger;

        public IdentifiedCommandHandler(IMediator mediator, 
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<T, TR>> logger)
        {
            _mediator = mediator;
            _requestManager = requestManager;
            _logger = logger;
        }
        
        public async Task<TR> Handle(IdentifiedCommand<T, TR> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await _requestManager.CreateRequestForCommandAsync<T>(request.Id);
                try
                {
                    var command = request.Command;
                    var commandName = string.Empty; // request.GetGenericTypeName();
                    string idProperty;
                    string commandId;

                    switch (command)
                    {
                        case CreateDocumentCommand createDocumentCommand:
                            idProperty = nameof(createDocumentCommand.PatientNo);
                            commandId = createDocumentCommand.PatientNo;
                            break;
                        default:
                            idProperty = "Id?";
                            commandId = "n/a";
                            break;
                    }
                    
                    _logger.LogInformation(
                        "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    var result = await _mediator.Send(command, cancellationToken);

                    _logger.LogInformation(
                        "----- Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        result,
                        commandName,
                        idProperty,
                        commandId,
                        command);
                    
                    return result;
                }
                catch
                {
                    return default;
                }
            }
            
            
        }

        protected virtual TR CreateResultForDuplicateRequest()
        {
            return default;
        }
    }
}
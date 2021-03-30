using System;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Behaviors
{
    public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly MedicalRecordContext _context;

        public TransactionPipelineBehavior(ILogger<TransactionPipelineBehavior<TRequest, TResponse>> logger,
            MedicalRecordContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            // TODO: CreateDocumentCommand GetDynamicTypeName function in order to get generic params name
            var typeName = string.Empty;
            try
            {
                if (_context.HasActiveTransaction())
                {
                    return await next();
                }

                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    await using var transaction = await _context.BeginTransactionAsync();
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);
                    
                    await _context.CommitTransactionAsync(transaction);

                });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}
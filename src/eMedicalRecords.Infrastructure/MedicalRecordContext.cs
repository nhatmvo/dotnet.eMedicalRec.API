using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eMedicalRecords.Infrastructure
{
    public class MedicalRecordContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        private readonly IMediator _mediator;
        
        public MedicalRecordContext(DbContextOptions<MedicalRecordContext> options) : base(options) { }

        public MedicalRecordContext(DbContextOptions<MedicalRecordContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public bool HasActiveTransaction() => _currentTransaction != null;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync();
            return true;
        }
        
        public async Task<IDbContextTransaction> StartTransactionAsync()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException("");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure.Configurations;
using eMedicalRecords.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;

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
            modelBuilder.ApplyConfiguration(new ControlEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ControlTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HeadingSetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HeadingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SectionDataEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
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
    
    public class MedicalRecordContextDesignFactory : IDesignTimeDbContextFactory<MedicalRecordContext>
    {
        public MedicalRecordContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../eMedicalRecords.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var dbConfiguration = config.Get<DbConfiguration>();
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbConfiguration.Hostname,
                Database = dbConfiguration.Database,
                Username = dbConfiguration.Username,
                Password = dbConfiguration.Password,
                Port = int.Parse(dbConfiguration.Port)
            };
                
            var optionsBuilder = new DbContextOptionsBuilder<MedicalRecordContext>()
                .UseNpgsql(builder.ConnectionString);

            return new MedicalRecordContext(optionsBuilder.Options);
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.SeedWorks;
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
    public class MedicalRecordContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;
        private readonly IMediator _mediator;
        
        public DbSet<ControlBase> Controls { get; set; }
        public DbSet<ControlType> ControlTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryData> EntryData { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<EntryData> SectionData { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        
        
        public MedicalRecordContext(DbContextOptions<MedicalRecordContext> options) : base(options) { }

        public MedicalRecordContext(DbContextOptions<MedicalRecordContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ControlEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ControlTextEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ControlTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntryDataEntityTypeConfiguration());
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
        
        public async Task<IDbContextTransaction> BeginTransactionAsync()
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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            // await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
    
    public class MedicalRecordContextDesignFactory : IDesignTimeDbContextFactory<MedicalRecordContext>
    {
        public MedicalRecordContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../eMedicalRecords.API"))
                .AddJsonFile("appsettings.json").Build();
            
            
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = "localhost",
                Database = "emr",
                Username = "pgdev",
                Password = "default",
                Port = 54321
            };
                
            var optionsBuilder = new DbContextOptionsBuilder<MedicalRecordContext>()
                .UseNpgsql(builder.ConnectionString);

            return new MedicalRecordContext(optionsBuilder.Options);
        }
    }
}
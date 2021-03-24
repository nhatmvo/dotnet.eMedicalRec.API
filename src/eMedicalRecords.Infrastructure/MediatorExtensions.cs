using System.Linq;
using System.Threading.Tasks;
using eMedicalRecords.Domain.SeedWorks;
using MediatR;

namespace eMedicalRecords.Infrastructure
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, MedicalRecordContext context)
        {
            var domainEntities = context.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            var entityEntries = domainEntities.ToList();

            var domainEvents = entityEntries
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
            
            entityEntries.ForEach(p => p.Entity.ClearDomainEvent());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
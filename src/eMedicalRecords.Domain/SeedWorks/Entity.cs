using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MediatR;

namespace eMedicalRecords.Domain.SeedWorks
{
    public abstract class Entity
    {
        Guid _id;

        public virtual Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        private List<INotification> _domainEvents;

        public ReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
    }
}
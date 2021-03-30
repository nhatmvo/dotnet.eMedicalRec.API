namespace eMedicalRecords.Domain.SeedWorks
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}
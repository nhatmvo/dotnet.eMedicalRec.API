namespace eMedicalRecords.Domain.SeedWorks
{
    public abstract class Enumeration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration) other).Id);
    }
}
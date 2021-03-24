using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.PatientAggregate
{
    public class IdentityType : Enumeration
    {
        public static IdentityType IdentityCard = new IdentityType(1, nameof(IdentityCard));
        public static IdentityType CitizenIdentification = new IdentityType(2, nameof(CitizenIdentification));
        public static IdentityType DriverLicense = new IdentityType(3, nameof(DriverLicense));
        public static IdentityType Passport = new IdentityType(4, nameof(Passport));
        public IdentityType(int id, string name) : base(id, name)
        {
        }
    }
}
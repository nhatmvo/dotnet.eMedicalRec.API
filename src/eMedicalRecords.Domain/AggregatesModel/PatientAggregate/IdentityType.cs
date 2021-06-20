using System;
using System.Collections.Generic;
using System.Linq;
using eMedicalRecords.Domain.Exceptions;
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

        public static IEnumerable<IdentityType> List() =>
            new[] {IdentityCard, CitizenIdentification, DriverLicense, Passport};

        public static IdentityType FromName(string name)
        {
            var type = List()
                .FirstOrDefault(t => String.Equals(t.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (type == null)
                throw new DomainException(
                    $"$Possible values for Element Type: {String.Join(",", List().Select(t => t.Name))}");

            return type;
        }

        public static IdentityType From(int id)
        {
            var type = List().FirstOrDefault(t => t.Id == id);
            if (type == null)
                throw new DomainException(
                    $"$Possible values for Element Type: {String.Join(",", List().Select(t => t.Name))}");

            return type;
        }
    }
}
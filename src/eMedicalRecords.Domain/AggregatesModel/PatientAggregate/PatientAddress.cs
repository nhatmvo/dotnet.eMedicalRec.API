using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.PatientAggregate
{
    public class PatientAddress : ValueObject
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressLine { get; set; }
        
        protected PatientAddress() {}

        public PatientAddress(string country, string city, string district, string addressLine)
        {
            Country = country;
            City = city;
            District = district;
            AddressLine = addressLine;
        }
    }
}
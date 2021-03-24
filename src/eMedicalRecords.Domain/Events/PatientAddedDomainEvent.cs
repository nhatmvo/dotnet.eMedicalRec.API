using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using MediatR;

namespace eMedicalRecords.Domain.Events
{
    public class PatientAddedDomainEvent : INotification
    {
        public Patient Patient { get; }
        public string IdentityNo { get; }
        public string Email { get; }

        public PatientAddedDomainEvent(Patient patient, string identityNo, string email)
        {
            Patient = patient;
            IdentityNo = identityNo;
            Email = email;
        }
    }
}
using System;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Patient
{
    public class CreatePatientCommand : IRequest<bool>
    {
        [DataMember] public string IdentityNo { get; set; }
        [DataMember] public int IdentityTypeId { get; set; }
        [DataMember] public string FirstName { get; set; }
        [DataMember] public string MiddleName { get; set; }
        [DataMember] public string LastName { get; set; }
        [DataMember] public string Country { get; set; }
        [DataMember] public string City { get; set; }
        [DataMember] public string District { get; set; }
        [DataMember] public string AddressLine { get; set; }
        [DataMember] public string PhoneNumber { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public DateTime DateOfBirth { get; set; }
        [DataMember] public bool HasInsurance { get; set; }
        [DataMember] public string Description { get; set; }
    }
}
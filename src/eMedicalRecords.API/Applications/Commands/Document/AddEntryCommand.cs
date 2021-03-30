using System;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class AddEntryCommand : IRequest<bool>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public Guid DocumentId { get; set; }
        [DataMember] public Guid PatientId { get; set; }
        [DataMember] public Guid TemplateId { get; set; }
    }
}
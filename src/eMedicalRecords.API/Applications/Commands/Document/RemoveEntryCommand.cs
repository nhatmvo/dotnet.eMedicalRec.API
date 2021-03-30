using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class RemoveEntryCommand : IRequest<bool>
    {
        [DataMember] public string EntryId { get; set; }
    }
}
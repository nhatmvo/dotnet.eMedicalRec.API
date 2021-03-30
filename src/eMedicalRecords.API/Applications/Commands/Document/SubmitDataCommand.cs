using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class SubmitDataCommand : IRequest<bool>
    {
        [DataMember] public List<EntryDataRequest> EntryDataRequests { get; set; }
    }

    public class EntryDataRequest
    {
        [DataMember] public Guid EntryId { get; set; }
        [DataMember] public Guid SectionId { get; set; }
        [DataMember] public string Value { get; set; }
    }
}
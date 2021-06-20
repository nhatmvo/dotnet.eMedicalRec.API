using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class PopulateDataCommand : IRequest<bool>
    {
        [DataMember] public Guid EntryId { get; set; }

        [DataMember] public List<EntryDataRequest> EntryDataRequests { get; set; }
    }

    public class EntryDataRequest
    {
        [DataMember] public Guid ElementId { get; set; }
        [DataMember] public string Value { get; set; }
    }
}
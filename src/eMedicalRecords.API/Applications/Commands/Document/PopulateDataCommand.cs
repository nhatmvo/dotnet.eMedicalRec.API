using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class PopulateDataCommand : IRequest<bool>
    {
        [DataMember] public Guid PatientId { get; set; }
        [DataMember] public Guid TemplateId { get; set; }
        [DataMember] public Guid DocumentId { get; set; }

        [DataMember] public List<EntryDataRequest> EntryDataRequests { get; set; }
    }

    public class EntryDataRequest
    {
        [DataMember] public Guid ElementId { get; set; }
        [DataMember] public List<string> Values { get; set; }
        [DataMember] public List<IFormFile> Files { get; set; }
}
}
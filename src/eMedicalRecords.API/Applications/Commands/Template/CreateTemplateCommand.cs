using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Template
{
    public class CreateTemplateCommand : IRequest<bool>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public bool IsDefault { get; set; }
        [DataMember] public List<SectionRequest> SectionRequests { get; set; }

    }

    public class SectionRequest
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string Tooltip { get; set; }
        [DataMember] public int KindId { get; set; }
        [DataMember] public List<string> KindValues { get; set; }
        [DataMember] public List<SectionRequest> ChildSections { get; set; }
    }
}
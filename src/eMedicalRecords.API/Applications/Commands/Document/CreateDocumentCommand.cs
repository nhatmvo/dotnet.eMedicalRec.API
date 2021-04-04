using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class CreateDocumentCommand : IRequest<bool>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string DepartmentName { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string PatientNo { get; set; }
    }
    
}
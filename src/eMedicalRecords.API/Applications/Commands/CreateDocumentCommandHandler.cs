using System.Runtime.Serialization;

namespace eMedicalRecords.API.Applications.Commands
{
    public class CreateDocumentCommandHandler
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string PatientCode { get; set; }
    }
}
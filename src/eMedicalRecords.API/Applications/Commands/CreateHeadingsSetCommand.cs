using System.Collections.Generic;
using System.Runtime.Serialization;

namespace eMedicalRecords.API.Applications.Commands
{
    public class CreateHeadingsSetCommand
    {
        [DataMember]
        public List<Heading> Headings { get; set; }
        

    }

    public class Heading
    {
        public string Type { get; set; }
    }
}
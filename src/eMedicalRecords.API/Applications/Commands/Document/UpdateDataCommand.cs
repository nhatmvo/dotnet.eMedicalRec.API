using System;
using System.Collections.Generic;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    public class UpdateDataCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public List<EntryDataRequest> EntryDataRequests { get; set; }
    }
}
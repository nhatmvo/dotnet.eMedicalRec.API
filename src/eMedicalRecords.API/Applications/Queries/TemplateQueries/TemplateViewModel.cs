using System;
using System.Collections.Generic;

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    public record Template
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<Element> Elements { get; set; } 
    }

    public record Element
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<Element> ChildElements { get; set; }
    }
}
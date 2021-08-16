using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using MongoDB.Bson;

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    public class TemplateQueries : ITemplateQueries
    {
        private readonly ITemplateService _templateService;

        public TemplateQueries(ITemplateService templateService) => 
            _templateService = templateService;
        
        public Task<BsonDocument> GetTemplateAsync(Guid id)
        {
            return _templateService.GetTemplateAsync(id);
        }
    }
}
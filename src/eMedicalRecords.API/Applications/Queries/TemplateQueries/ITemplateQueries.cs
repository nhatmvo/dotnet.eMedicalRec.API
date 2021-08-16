using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using MongoDB.Bson;

namespace eMedicalRecords.API.Applications.Queries.TemplateQueries
{
    public interface ITemplateQueries
    {
        public Task<BsonDocument> GetTemplateAsync(Guid id);
    }
}
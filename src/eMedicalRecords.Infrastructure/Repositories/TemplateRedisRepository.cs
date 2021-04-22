using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Domain.SeedWorks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace eMedicalRecords.Infrastructure.Repositories
{
    public class TemplateRedisRepository : ITemplateRedisRepository
    {
        private readonly ILogger<TemplateRedisRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        
        public TemplateRedisRepository(ILogger<TemplateRedisRepository> logger,
            ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase();
        }
        
        public async Task<bool> UpsertTemplateAsync(string templateId, string templateStructure)
        {
            var data = await _database.StringGetAsync(templateId);
            if (!data.IsNullOrEmpty)
                await _database.KeyDeleteAsync(templateId);
                
            return await _database.StringSetAsync(templateId, templateStructure);
        }

        public async Task<string> GetTemplateAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : data.ToString();
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}
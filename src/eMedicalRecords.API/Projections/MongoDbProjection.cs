using System;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace eMedicalRecords.API.Projections
{
    public class MongoDbProjection : IStateProjection
    {
        private readonly IConfiguration _configuration;
        private readonly ITemplateRepository _templateRepository;
        private readonly ITemplateService _templateService;

        public MongoDbProjection(IConfiguration configuration, ITemplateRepository templateRepository,
            ITemplateService templateService)
        {
            _configuration = configuration;
            _templateRepository = templateRepository;
            _templateService = templateService;
        }
        
        public async Task SubscribeTemplateState()
        {
            var postgresConfig = _configuration.Get<DbConfiguration>();
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = postgresConfig.PostgresHostname,
                Database = postgresConfig.PostgresDatabase,
                Username = postgresConfig.PostgresUsername,
                Password = postgresConfig.PostgresPassword,
                Port = int.Parse(postgresConfig.PostgresPort)
            };

            await using var conn = new NpgsqlConnection(builder.ConnectionString);
            await conn.OpenAsync();

            conn.Notification += async (s, e) => await TemplateChangesNotificationHandler(s, e);

            await using var cmd = new NpgsqlCommand("LISTEN templatechanges;", conn);
            cmd.ExecuteNonQuery();
            while (true)
            {
                await conn.WaitAsync();
            }
        }

        private async Task TemplateChangesNotificationHandler(object sender, NpgsqlNotificationEventArgs args)
        {
            var payloadAsJson = JObject.Parse(args.Payload);
            var templateIdChanged = Guid.Parse(payloadAsJson["id"].ToString());
            var templateOperation = payloadAsJson["action"].ToString();

            switch (templateOperation)
            {
                case "INSERT":
                    var templateToInsert = await _templateRepository.GetTemplateById(templateIdChanged);
                    templateToInsert.RemoveUpperLevelStructure();
                    await _templateService.AddTemplateAsync(templateToInsert);
                    break;
                case "UPDATE":
                    var templateToUpdate = await _templateRepository.GetTemplateById(templateIdChanged);
                    await _templateService.UpdateTemplateAsync(templateIdChanged, templateToUpdate);
                    break;
                case "DELETE":
                    await _templateService.DeleteTemplateAsync(templateIdChanged);
                    break;
            }
        }
        
        public void CreateSubscription()
        {
            //TODO: Add startup store procedure as well as trigger for Projection Engine (might run as part of the migration process)
        }
    }
}
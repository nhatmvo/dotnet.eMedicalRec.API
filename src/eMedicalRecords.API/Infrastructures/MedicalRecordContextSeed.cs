using System;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using Polly.Retry;

namespace eMedicalRecords.API.Infrastructures
{
    public class MedicalRecordContextSeed
    {
        public async Task SeedAsync(MedicalRecordContext context, ILogger<MedicalRecordContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(MedicalRecordContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                await using (context)
                {
                    await context.Database.MigrateAsync();
                }
            });
        }
        
        private AsyncRetryPolicy CreatePolicy(ILogger<MedicalRecordContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<PostgresException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception,
                            "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}",
                            prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
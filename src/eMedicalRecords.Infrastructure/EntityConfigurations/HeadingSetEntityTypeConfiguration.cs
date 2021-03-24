using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class HeadingSetEntityTypeConfiguration : IEntityTypeConfiguration<HeadingSet>
    {
        public void Configure(EntityTypeBuilder<HeadingSet> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
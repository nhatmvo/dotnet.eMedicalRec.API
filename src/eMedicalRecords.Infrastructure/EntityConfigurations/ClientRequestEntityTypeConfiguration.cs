using eMedicalRecords.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> builder)
        {
            builder.ToTable("requests").HasKey(c => c.Id);
            builder.Property(b => b.Id)
                .HasColumnName("id");
            builder.Property(b => b.Name)
                .HasColumnName("name");
            builder.Property(b => b.Time)
                .HasColumnName("time");
        }
    }
}
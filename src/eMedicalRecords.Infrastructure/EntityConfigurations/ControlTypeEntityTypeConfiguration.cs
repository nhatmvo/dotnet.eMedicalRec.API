using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ControlTypeEntityTypeConfiguration : IEntityTypeConfiguration<ControlType>
    {
        public void Configure(EntityTypeBuilder<ControlType> builder)
        {
            builder.ToTable("mre_control_types");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(b => b.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
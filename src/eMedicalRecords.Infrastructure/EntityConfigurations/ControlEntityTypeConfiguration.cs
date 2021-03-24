using System;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ControlEntityTypeConfiguration : IEntityTypeConfiguration<Control>
    {
        public void Configure(EntityTypeBuilder<Control> builder)
        {
            builder.ToTable("mre_control");

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property<Guid>("_recordAttributeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("record_attribute_id")
                .IsRequired();

            builder.HasOne(b => b.RecordAttribute)
                .WithMany()
                .HasForeignKey("_recordAttributeId");
            
        }
    }
}
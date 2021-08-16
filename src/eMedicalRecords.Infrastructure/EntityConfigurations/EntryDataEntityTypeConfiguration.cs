using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class EntryDataEntityTypeConfiguration : IEntityTypeConfiguration<EntryData>
    {
        public void Configure(EntityTypeBuilder<EntryData> builder)
        {
            builder.ToTable("document_entry_data");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id");
            
            builder.Property<Guid>("_elementId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("element_id")
                .IsRequired();

            builder.Property<Guid>("_entryId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("entry_id")
                .IsRequired();
            
            builder.Property<List<string>>("_values")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("values")
                .IsRequired();

            builder.HasOne<ElementBase>()
                .WithMany()
                .HasForeignKey("_elementId")
                .IsRequired();

            builder.HasOne(b => b.Entry)
                .WithMany()
                .HasForeignKey("_entryId")
                .IsRequired();
        }
    }
}
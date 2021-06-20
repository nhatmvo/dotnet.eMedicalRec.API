using System;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class EntryEntityTypeConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.ToTable("document_entry");

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property<Guid>("_headingSetId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("heading_set_id")
                .IsRequired();

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired(false);

            var navigation = builder.Metadata.FindNavigation(nameof(Entry.RecordValues));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Template>()
                .WithMany()
                .HasForeignKey("_templateId");
        }
    }
}
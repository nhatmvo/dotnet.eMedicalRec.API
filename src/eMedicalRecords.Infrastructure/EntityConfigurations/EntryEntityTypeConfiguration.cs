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

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property<Guid>("_templateId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("template_id")
                .IsRequired();
            
            builder.Property<Guid>("_documentId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("document_id");

            builder.Property<DateTime>("_createdDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("created_date");

            var navigation = builder.Metadata.FindNavigation(nameof(Entry.RecordValues));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Template>()
                .WithMany()
                .HasForeignKey("_templateId");

            builder.HasMany<EntryData>()
                .WithOne(b => b.Entry)
                .HasForeignKey("_entryId");
        }
    }
}
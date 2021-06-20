using System;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("document");

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Id).HasColumnName("id");

            builder.Property<DateTime>("_createdDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("created_date")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Document.DocumentEntries));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<DateTime?>("_updatedDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("updated_date")
                .IsRequired(false);
        }
    }
}
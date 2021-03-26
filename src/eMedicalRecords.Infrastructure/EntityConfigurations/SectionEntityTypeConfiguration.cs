using System;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class SectionEntityTypeConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("mre_sections");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property<Guid?>("_parentSectionId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("parent_section_id")
                .IsRequired(false);

            builder.Property<int>("_controlTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("control_type_id")
                .IsRequired();
            
            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property<string>("_tooltip")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("tooltip")
                .IsRequired(false);

            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired(false);

            builder.HasOne(b => b.ParentSection)
                .WithMany()
                .HasForeignKey("_parentSectionId");

            builder.HasOne(b => b.ControlType)
                .WithMany()
                .HasForeignKey("_controlTypeId");
        }
    }
}
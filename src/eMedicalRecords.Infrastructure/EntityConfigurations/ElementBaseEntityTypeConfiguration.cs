using System;
using System.Collections.Generic;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ElementBaseEntityTypeConfiguration : IEntityTypeConfiguration<ElementBase>
    {
        public void Configure(EntityTypeBuilder<ElementBase> builder)
        {
            builder.ToTable("template_element_base");

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);
            
            builder.Property<Guid?>("_parentElementId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("parent_element_id")
                .IsRequired(false);

            builder.Property<int>("_elementTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("element_type_id")
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

            builder.Property<Guid>("_templateId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("template_id")
                .IsRequired();

            builder.HasOne(b => b.ElementType)
                .WithMany()
                .HasForeignKey("_elementTypeId");
            
            builder.HasMany(b => b.ChildElements)
                .WithOne()
                .HasForeignKey("_parentElementId");

        }
    }
}
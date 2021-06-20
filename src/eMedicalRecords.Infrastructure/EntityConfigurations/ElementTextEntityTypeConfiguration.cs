using System.ComponentModel.DataAnnotations.Schema;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ElementTextEntityTypeConfiguration : IEntityTypeConfiguration<ElementText>
    {
        public void Configure(EntityTypeBuilder<ElementText> builder)
        {
            builder.ToTable("template_element_text");

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("element_base_id");
            
            builder.Property<int?>("_maximumLength")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("maximum_length")
                .HasDefaultValue(-1)
                .IsRequired(false);

            builder.Property<int?>("_minimumLength")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("minimum_length")
                .HasDefaultValue(-1)
                .IsRequired(false);

            builder.Property<int?>("_textRestrictionLevel")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("text_restriction_level")
                .HasDefaultValue(-1)
                .IsRequired(false);

            builder.Property<string>("_customExpression")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("custom_expression")
                .HasDefaultValue(string.Empty)
                .IsRequired();
        }
    }
}
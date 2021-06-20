using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    using Domain.AggregatesModel.TemplateAggregate;

    public class ElementTypeEntityTypeConfiguration : IEntityTypeConfiguration<ElementType>
    {
        public void Configure(EntityTypeBuilder<ElementType> builder)
        {
            builder.ToTable("template_element_type");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property(b => b.Name)
                .HasColumnName("name");

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
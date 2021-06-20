using System.Collections.Generic;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class ElementRadioButtonEntityTypeConfiguration : IEntityTypeConfiguration<ElementRadioButton>
    {
        public void Configure(EntityTypeBuilder<ElementRadioButton> builder)
        {
            builder.ToTable("template_element_radiobutton");
            
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("element_base_id");
            
            builder.Property<List<string>>("_options")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("options")
                .IsRequired(false);
        }
    }
}
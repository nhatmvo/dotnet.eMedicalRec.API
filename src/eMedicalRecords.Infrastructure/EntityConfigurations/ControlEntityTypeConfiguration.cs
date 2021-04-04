using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    using Domain.AggregatesModel.TemplateAggregate;

    public class ControlEntityTypeConfiguration : IEntityTypeConfiguration<ControlBase>
    {
        public virtual void Configure(EntityTypeBuilder<ControlBase> builder)
        {
            builder.ToTable("mre_control_base")
                .HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Ignore(b => b.DomainEvents);
            
            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property<Guid>("_sectionId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("section_id")
                .IsRequired();

            builder.HasOne(b => b.Section)
                .WithMany()
                .HasForeignKey("_sectionId");
        }
    }
}
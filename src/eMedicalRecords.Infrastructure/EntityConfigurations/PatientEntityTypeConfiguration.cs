using System;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(b => b.Id);

            builder.HasIndex("_identityNo")
                .IsUnique();
            
            builder.Property<string>("_identityNo")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("identity_no")
                .IsRequired();

            builder.OwnsOne(o => o.PatientAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property<string>("_firstName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("first_name")
                .IsRequired();
            
            builder.Property<string>("_middleName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("middle_name")
                .IsRequired(false);
            
            builder.Property<string>("_lastName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("last_name")
                .IsRequired();
            
            builder.Property<string>("_email")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("email")
                .IsRequired();
            
            builder.Property<string>("_phoneNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("phone_number")
                .IsRequired();
            
            builder.Property<DateTime>("_dateOfBirth")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("date_of_birth")
                .IsRequired();
            
            builder.Property<bool>("_hasInsurance")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("has_insurance")
                .IsRequired();
            
            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}
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
            builder.ToTable("patient");

            builder.Ignore(b => b.DomainEvents);

            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.HasIndex(b => b.PatientNo)
                .IsUnique();
            
            builder.HasIndex("_identityNo")
                .IsUnique();

            builder.Property(b => b.PatientNo)
                .HasColumnName("patient_document_no")
                .IsRequired();
            
            builder.Property<string>("_identityNo")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("identity_no")
                .IsRequired();

            builder.OwnsOne(o => o.PatientAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();
            
            builder.Property<bool>("_gender")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("gender")
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
            
            builder.Property<DateTime>("_admissionDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("admission_date")
                .IsRequired();
            
            builder.Property<DateTime>("_surgeryDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("surgery_date")
                .IsRequired();
            
            builder.Property<DateTime>("_examinationDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("examination_date")
                .IsRequired();
        }
    }
}
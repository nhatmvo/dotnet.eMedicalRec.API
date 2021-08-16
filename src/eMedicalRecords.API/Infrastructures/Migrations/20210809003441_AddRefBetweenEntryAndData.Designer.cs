﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eMedicalRecords.Infrastructure;

namespace eMedicalRecords.Infrastructure.Migrations
{
    [DbContext(typeof(MedicalRecordContext))]
    [Migration("20210809003441_AddRefBetweenEntryAndData")]
    partial class AddRefBetweenEntryAndData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("_createdDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<Guid>("_patientId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_id");

                    b.Property<DateTime?>("_updatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("_patientId");

                    b.ToTable("document");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("_createdDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<Guid>("_documentId")
                        .HasColumnType("uuid")
                        .HasColumnName("document_id");

                    b.Property<Guid>("_templateId")
                        .HasColumnType("uuid")
                        .HasColumnName("template_id");

                    b.HasKey("Id");

                    b.HasIndex("_documentId");

                    b.HasIndex("_templateId");

                    b.ToTable("document_entry");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.EntryData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("_elementId")
                        .HasColumnType("uuid")
                        .HasColumnName("element_id");

                    b.Property<Guid>("_entryId")
                        .HasColumnType("uuid")
                        .HasColumnName("entry_id");

                    b.Property<Guid?>("_entry_id")
                        .HasColumnType("uuid");

                    b.Property<string>("_value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("_elementId");

                    b.HasIndex("_entryId");

                    b.HasIndex("_entry_id");

                    b.ToTable("document_entry_data");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.IdentityType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("identity_type");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int?>("IdentityTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("PatientNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("patient_document_no");

                    b.Property<DateTime>("_admissionDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("admission_date");

                    b.Property<DateTime>("_dateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("_email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTime>("_examinationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("examination_date");

                    b.Property<bool>("_gender")
                        .HasColumnType("boolean")
                        .HasColumnName("gender");

                    b.Property<bool>("_hasInsurance")
                        .HasColumnType("boolean")
                        .HasColumnName("has_insurance");

                    b.Property<string>("_identityNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_no");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("_phoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<DateTime>("_surgeryDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("surgery_date");

                    b.HasKey("Id");

                    b.HasIndex("IdentityTypeId");

                    b.HasIndex("PatientNo")
                        .IsUnique();

                    b.HasIndex("_identityNo")
                        .IsUnique();

                    b.ToTable("patient");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("element_base_id");

                    b.Property<string>("_description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("_elementTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("element_type_id");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("_parentElementId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_element_id");

                    b.Property<Guid>("_templateId")
                        .HasColumnType("uuid")
                        .HasColumnName("template_id");

                    b.Property<string>("_tooltip")
                        .HasColumnType("text")
                        .HasColumnName("tooltip");

                    b.HasKey("Id");

                    b.HasIndex("_elementTypeId");

                    b.HasIndex("_parentElementId");

                    b.HasIndex("_templateId");

                    b.ToTable("template_element_base");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("template_element_type");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("_isDefault")
                        .HasColumnType("boolean")
                        .HasColumnName("is_default");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("template");
                });

            modelBuilder.Entity("eMedicalRecords.Infrastructure.Securities.AccountModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("account");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementCheckbox", b =>
                {
                    b.HasBaseType("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase");

                    b.Property<List<string>>("_options")
                        .HasColumnType("text[]")
                        .HasColumnName("options");

                    b.ToTable("template_element_checkbox");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementRadioButton", b =>
                {
                    b.HasBaseType("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase");

                    b.Property<List<string>>("_options")
                        .HasColumnType("text[]")
                        .HasColumnName("options");

                    b.ToTable("template_element_radiobutton");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementText", b =>
                {
                    b.HasBaseType("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase");

                    b.Property<string>("_customExpression")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("")
                        .HasColumnName("custom_expression");

                    b.Property<int?>("_maximumLength")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(-1)
                        .HasColumnName("maximum_length");

                    b.Property<int?>("_minimumLength")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(-1)
                        .HasColumnName("minimum_length");

                    b.Property<int?>("_textRestrictionLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(-1)
                        .HasColumnName("text_restriction_level");

                    b.ToTable("template_element_text");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Document", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.Patient", null)
                        .WithMany()
                        .HasForeignKey("_patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Entry", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Document", null)
                        .WithMany("DocumentEntries")
                        .HasForeignKey("_documentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.Template", null)
                        .WithMany()
                        .HasForeignKey("_templateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.EntryData", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", null)
                        .WithMany()
                        .HasForeignKey("_elementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Entry", "Entry")
                        .WithMany()
                        .HasForeignKey("_entryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Entry", null)
                        .WithMany("RecordValues")
                        .HasForeignKey("_entry_id");

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.Patient", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.IdentityType", "IdentityType")
                        .WithMany()
                        .HasForeignKey("IdentityTypeId");

                    b.OwnsOne("eMedicalRecords.Domain.AggregatesModel.PatientAggregate.PatientAddress", "PatientAddress", b1 =>
                        {
                            b1.Property<Guid>("PatientId")
                                .HasColumnType("uuid");

                            b1.Property<string>("AddressLine")
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .HasColumnType("text");

                            b1.HasKey("PatientId");

                            b1.ToTable("patient");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("IdentityType");

                    b.Navigation("PatientAddress");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementType", "ElementType")
                        .WithMany()
                        .HasForeignKey("_elementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", null)
                        .WithMany("ChildElements")
                        .HasForeignKey("_parentElementId");

                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.Template", null)
                        .WithMany("Elements")
                        .HasForeignKey("_templateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ElementType");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementCheckbox", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", null)
                        .WithOne()
                        .HasForeignKey("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementCheckbox", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementRadioButton", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", null)
                        .WithOne()
                        .HasForeignKey("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementRadioButton", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementText", b =>
                {
                    b.HasOne("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", null)
                        .WithOne()
                        .HasForeignKey("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementText", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Document", b =>
                {
                    b.Navigation("DocumentEntries");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.DocumentAggregate.Entry", b =>
                {
                    b.Navigation("RecordValues");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.ElementBase", b =>
                {
                    b.Navigation("ChildElements");
                });

            modelBuilder.Entity("eMedicalRecords.Domain.AggregatesModel.TemplateAggregate.Template", b =>
                {
                    b.Navigation("Elements");
                });
#pragma warning restore 612, 618
        }
    }
}

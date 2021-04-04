using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "control_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "identity_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mr_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mre_template",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientNo = table.Column<string>(type: "text", nullable: true),
                    PatientAddress_Country = table.Column<string>(type: "text", nullable: true),
                    PatientAddress_City = table.Column<string>(type: "text", nullable: true),
                    PatientAddress_District = table.Column<string>(type: "text", nullable: true),
                    PatientAddress_AddressLine = table.Column<string>(type: "text", nullable: true),
                    IdentityTypeId = table.Column<int>(type: "integer", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    has_insurance = table.Column<bool>(type: "boolean", nullable: false),
                    identity_no = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id);
                    table.ForeignKey(
                        name: "FK_patients_identity_type_IdentityTypeId",
                        column: x => x.IdentityTypeId,
                        principalTable: "identity_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mr_entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    heading_set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    _templateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mr_entries_mr_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "mr_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mr_entries_mre_template__templateId",
                        column: x => x._templateId,
                        principalTable: "mre_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mre_sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    control_type_id = table.Column<int>(type: "integer", nullable: false),
                    AdditionalControlInformationId = table.Column<Guid>(type: "uuid", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    options = table.Column<List<string>>(type: "text[]", nullable: true),
                    parent_section_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tooltip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_sections_control_types_control_type_id",
                        column: x => x.control_type_id,
                        principalTable: "control_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mre_sections_mre_sections_parent_section_id",
                        column: x => x.parent_section_id,
                        principalTable: "mre_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mre_sections_mre_template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "mre_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mre_control_base",
                columns: table => new
                {
                    control_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_control_base", x => x.control_base_id);
                    table.ForeignKey(
                        name: "FK_mre_control_base_mre_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "mre_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mre_entry_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_entry_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_entry_data_mr_entries_entry_id",
                        column: x => x.entry_id,
                        principalTable: "mr_entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mre_entry_data_mr_entries_EntryId1",
                        column: x => x.EntryId1,
                        principalTable: "mr_entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mre_entry_data_mre_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "mre_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mre_control_text",
                columns: table => new
                {
                    control_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    custom_expression = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    maximum_length = table.Column<int>(type: "integer", nullable: true, defaultValue: -1),
                    minimum_length = table.Column<int>(type: "integer", nullable: true, defaultValue: -1),
                    text_restriction_level = table.Column<int>(type: "integer", nullable: true, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_control_text", x => x.control_base_id);
                    table.ForeignKey(
                        name: "FK_mre_control_text_mre_control_base_control_base_id",
                        column: x => x.control_base_id,
                        principalTable: "mre_control_base",
                        principalColumn: "control_base_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mr_entries__templateId",
                table: "mr_entries",
                column: "_templateId");

            migrationBuilder.CreateIndex(
                name: "IX_mr_entries_DocumentId",
                table: "mr_entries",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_mre_control_base_section_id",
                table: "mre_control_base",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_entry_data_entry_id",
                table: "mre_entry_data",
                column: "entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_entry_data_EntryId1",
                table: "mre_entry_data",
                column: "EntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_mre_entry_data_section_id",
                table: "mre_entry_data",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_AdditionalControlInformationId",
                table: "mre_sections",
                column: "AdditionalControlInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_control_type_id",
                table: "mre_sections",
                column: "control_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_parent_section_id",
                table: "mre_sections",
                column: "parent_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_TemplateId",
                table: "mre_sections",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_identity_no",
                table: "patients",
                column: "identity_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_IdentityTypeId",
                table: "patients",
                column: "IdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_PatientNo",
                table: "patients",
                column: "PatientNo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_mre_sections_mre_control_base_AdditionalControlInformationId",
                table: "mre_sections",
                column: "AdditionalControlInformationId",
                principalTable: "mre_control_base",
                principalColumn: "control_base_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mre_sections_mre_template_TemplateId",
                table: "mre_sections");

            migrationBuilder.DropForeignKey(
                name: "FK_mre_control_base_mre_sections_section_id",
                table: "mre_control_base");

            migrationBuilder.DropTable(
                name: "mre_control_text");

            migrationBuilder.DropTable(
                name: "mre_entry_data");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "mr_entries");

            migrationBuilder.DropTable(
                name: "identity_type");

            migrationBuilder.DropTable(
                name: "mr_documents");

            migrationBuilder.DropTable(
                name: "mre_template");

            migrationBuilder.DropTable(
                name: "mre_sections");

            migrationBuilder.DropTable(
                name: "control_types");

            migrationBuilder.DropTable(
                name: "mre_control_base");
        }
    }
}

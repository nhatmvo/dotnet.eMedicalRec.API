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
                name: "document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.id);
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
                name: "request",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "template",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "template_element_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_element_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patient",
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
                    table.PrimaryKey("PK_patient", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_identity_type_IdentityTypeId",
                        column: x => x.IdentityTypeId,
                        principalTable: "identity_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "document_entry",
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
                    table.PrimaryKey("PK_document_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_entry_document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_entry_template__templateId",
                        column: x => x._templateId,
                        principalTable: "template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_element_base",
                columns: table => new
                {
                    element_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    element_type_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    parent_element_id = table.Column<Guid>(type: "uuid", nullable: true),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tooltip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_element_base", x => x.element_base_id);
                    table.ForeignKey(
                        name: "FK_template_element_base_template_element_base_parent_element_~",
                        column: x => x.parent_element_id,
                        principalTable: "template_element_base",
                        principalColumn: "element_base_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_template_element_base_template_element_type_element_type_id",
                        column: x => x.element_type_id,
                        principalTable: "template_element_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_template_element_base_template_template_id",
                        column: x => x.template_id,
                        principalTable: "template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document_entry_data",
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
                    table.PrimaryKey("PK_document_entry_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_document_entry_data_document_entry_entry_id",
                        column: x => x.entry_id,
                        principalTable: "document_entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_document_entry_data_document_entry_EntryId1",
                        column: x => x.EntryId1,
                        principalTable: "document_entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_entry_data_template_element_base_section_id",
                        column: x => x.section_id,
                        principalTable: "template_element_base",
                        principalColumn: "element_base_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_element_checkbox",
                columns: table => new
                {
                    element_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    options = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_element_checkbox", x => x.element_base_id);
                    table.ForeignKey(
                        name: "FK_template_element_checkbox_template_element_base_element_bas~",
                        column: x => x.element_base_id,
                        principalTable: "template_element_base",
                        principalColumn: "element_base_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_element_radiobutton",
                columns: table => new
                {
                    element_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    options = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_element_radiobutton", x => x.element_base_id);
                    table.ForeignKey(
                        name: "FK_template_element_radiobutton_template_element_base_element_~",
                        column: x => x.element_base_id,
                        principalTable: "template_element_base",
                        principalColumn: "element_base_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_element_text",
                columns: table => new
                {
                    element_base_id = table.Column<Guid>(type: "uuid", nullable: false),
                    custom_expression = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    maximum_length = table.Column<int>(type: "integer", nullable: true, defaultValue: -1),
                    minimum_length = table.Column<int>(type: "integer", nullable: true, defaultValue: -1),
                    text_restriction_level = table.Column<int>(type: "integer", nullable: true, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_element_text", x => x.element_base_id);
                    table.ForeignKey(
                        name: "FK_template_element_text_template_element_base_element_base_id",
                        column: x => x.element_base_id,
                        principalTable: "template_element_base",
                        principalColumn: "element_base_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_entry__templateId",
                table: "document_entry",
                column: "_templateId");

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_DocumentId",
                table: "document_entry",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_data_entry_id",
                table: "document_entry_data",
                column: "entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_data_EntryId1",
                table: "document_entry_data",
                column: "EntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_data_section_id",
                table: "document_entry_data",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_identity_no",
                table: "patient",
                column: "identity_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patient_IdentityTypeId",
                table: "patient",
                column: "IdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_patient_PatientNo",
                table: "patient",
                column: "PatientNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_template_element_base_element_type_id",
                table: "template_element_base",
                column: "element_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_template_element_base_parent_element_id",
                table: "template_element_base",
                column: "parent_element_id");

            migrationBuilder.CreateIndex(
                name: "IX_template_element_base_template_id",
                table: "template_element_base",
                column: "template_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_entry_data");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "request");

            migrationBuilder.DropTable(
                name: "template_element_checkbox");

            migrationBuilder.DropTable(
                name: "template_element_radiobutton");

            migrationBuilder.DropTable(
                name: "template_element_text");

            migrationBuilder.DropTable(
                name: "document_entry");

            migrationBuilder.DropTable(
                name: "identity_type");

            migrationBuilder.DropTable(
                name: "template_element_base");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "template_element_type");

            migrationBuilder.DropTable(
                name: "template");
        }
    }
}

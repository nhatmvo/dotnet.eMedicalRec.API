using System;
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
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "identity_type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mr_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    updated_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mre_heading_sets",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    is_default = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_heading_sets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mr_entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    heading_set_id = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
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
                        name: "FK_mr_entries_mre_heading_sets_heading_set_id",
                        column: x => x.heading_set_id,
                        principalTable: "mre_heading_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mre_headings",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    HeadingSetId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_headings", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_headings_mre_heading_sets_HeadingSetId",
                        column: x => x.HeadingSetId,
                        principalTable: "mre_heading_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mre_sections",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    parent_section_id = table.Column<Guid>(nullable: true),
                    control_type_id = table.Column<int>(nullable: false),
                    HeadingId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    tooltip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_sections_mre_headings_HeadingId",
                        column: x => x.HeadingId,
                        principalTable: "mre_headings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "mre_control",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    record_attribute_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_control", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_control_mre_sections_record_attribute_id",
                        column: x => x.record_attribute_id,
                        principalTable: "mre_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mre_section_data",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    section_id = table.Column<Guid>(nullable: false),
                    entry_id = table.Column<Guid>(nullable: false),
                    EntryId1 = table.Column<Guid>(nullable: true),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mre_section_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_mre_section_data_mr_entries_EntryId1",
                        column: x => x.EntryId1,
                        principalTable: "mr_entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mre_section_data_mr_entries_entry_id",
                        column: x => x.entry_id,
                        principalTable: "mr_entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mre_section_data_mre_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "mre_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mr_entries_DocumentId",
                table: "mr_entries",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_mr_entries_heading_set_id",
                table: "mr_entries",
                column: "heading_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_control_record_attribute_id",
                table: "mre_control",
                column: "record_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_headings_HeadingSetId",
                table: "mre_headings",
                column: "HeadingSetId");

            migrationBuilder.CreateIndex(
                name: "IX_mre_section_data_EntryId1",
                table: "mre_section_data",
                column: "EntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_mre_section_data_entry_id",
                table: "mre_section_data",
                column: "entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_section_data_section_id",
                table: "mre_section_data",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_HeadingId",
                table: "mre_sections",
                column: "HeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_control_type_id",
                table: "mre_sections",
                column: "control_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_mre_sections_parent_section_id",
                table: "mre_sections",
                column: "parent_section_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identity_type");

            migrationBuilder.DropTable(
                name: "mre_control");

            migrationBuilder.DropTable(
                name: "mre_section_data");

            migrationBuilder.DropTable(
                name: "mr_entries");

            migrationBuilder.DropTable(
                name: "mre_sections");

            migrationBuilder.DropTable(
                name: "mr_documents");

            migrationBuilder.DropTable(
                name: "mre_headings");

            migrationBuilder.DropTable(
                name: "control_types");

            migrationBuilder.DropTable(
                name: "mre_heading_sets");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class ChangeDocumentEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_data_template_element_base_section_id",
                table: "document_entry_data");

            migrationBuilder.RenameColumn(
                name: "section_id",
                table: "document_entry_data",
                newName: "element_id");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry_data_section_id",
                table: "document_entry_data",
                newName: "IX_document_entry_data_element_id");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_data_template_element_base_element_id",
                table: "document_entry_data",
                column: "element_id",
                principalTable: "template_element_base",
                principalColumn: "element_base_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_data_template_element_base_element_id",
                table: "document_entry_data");

            migrationBuilder.RenameColumn(
                name: "element_id",
                table: "document_entry_data",
                newName: "section_id");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry_data_element_id",
                table: "document_entry_data",
                newName: "IX_document_entry_data_section_id");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_data_template_element_base_section_id",
                table: "document_entry_data",
                column: "section_id",
                principalTable: "template_element_base",
                principalColumn: "element_base_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

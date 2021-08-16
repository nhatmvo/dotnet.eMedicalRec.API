using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class AddRefBetweenEntryAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_data_document_entry_EntryId1",
                table: "document_entry_data");

            migrationBuilder.RenameColumn(
                name: "EntryId1",
                table: "document_entry_data",
                newName: "_entry_id");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry_data_EntryId1",
                table: "document_entry_data",
                newName: "IX_document_entry_data__entry_id");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_data_document_entry__entry_id",
                table: "document_entry_data",
                column: "_entry_id",
                principalTable: "document_entry",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_data_document_entry__entry_id",
                table: "document_entry_data");

            migrationBuilder.RenameColumn(
                name: "_entry_id",
                table: "document_entry_data",
                newName: "EntryId1");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry_data__entry_id",
                table: "document_entry_data",
                newName: "IX_document_entry_data_EntryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_data_document_entry_EntryId1",
                table: "document_entry_data",
                column: "EntryId1",
                principalTable: "document_entry",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

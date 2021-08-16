using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class AddReferenceEntryForDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_document_DocumentId",
                table: "document_entry");

            migrationBuilder.DropIndex(
                name: "IX_document_entry_DocumentId",
                table: "document_entry");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "document_entry");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "document_entry",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "heading_set_id",
                table: "document_entry",
                newName: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_document_id",
                table: "document_entry",
                column: "document_id");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_document_document_id",
                table: "document_entry",
                column: "document_id",
                principalTable: "document",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_document_document_id",
                table: "document_entry");

            migrationBuilder.DropIndex(
                name: "IX_document_entry_document_id",
                table: "document_entry");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "document_entry",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "document_id",
                table: "document_entry",
                newName: "heading_set_id");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "document_entry",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_entry_DocumentId",
                table: "document_entry",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_document_DocumentId",
                table: "document_entry",
                column: "DocumentId",
                principalTable: "document",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class AddCreatedDateEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_template__templateId",
                table: "document_entry");

            migrationBuilder.DropColumn(
                name: "description",
                table: "document_entry");

            migrationBuilder.DropColumn(
                name: "name",
                table: "document_entry");

            migrationBuilder.RenameColumn(
                name: "_templateId",
                table: "document_entry",
                newName: "template_id");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry__templateId",
                table: "document_entry",
                newName: "IX_document_entry_template_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "document_entry",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_template_template_id",
                table: "document_entry",
                column: "template_id",
                principalTable: "template",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_entry_template_template_id",
                table: "document_entry");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "document_entry");

            migrationBuilder.RenameColumn(
                name: "template_id",
                table: "document_entry",
                newName: "_templateId");

            migrationBuilder.RenameIndex(
                name: "IX_document_entry_template_id",
                table: "document_entry",
                newName: "IX_document_entry__templateId");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "document_entry",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "document_entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_document_entry_template__templateId",
                table: "document_entry",
                column: "_templateId",
                principalTable: "template",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

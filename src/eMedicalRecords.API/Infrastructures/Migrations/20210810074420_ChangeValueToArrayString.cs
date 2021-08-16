using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMedicalRecords.Infrastructure.Migrations
{
    public partial class ChangeValueToArrayString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "document_entry_data");

            migrationBuilder.AddColumn<List<string>>(
                name: "values",
                table: "document_entry_data",
                type: "text[]",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "values",
                table: "document_entry_data");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "document_entry_data",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

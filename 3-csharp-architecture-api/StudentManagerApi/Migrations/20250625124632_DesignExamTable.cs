using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagerApi.Migrations
{
    /// <inheritdoc />
    public partial class DesignExamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Exams",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Exams",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Exams",
                newName: "Name");
        }
    }
}

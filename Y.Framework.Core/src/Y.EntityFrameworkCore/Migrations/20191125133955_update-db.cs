using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Experiences",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Experiences");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Experiences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "Experiences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

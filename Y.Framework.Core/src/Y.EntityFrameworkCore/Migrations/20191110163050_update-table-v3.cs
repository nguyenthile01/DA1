using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatetablev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "DesiredLocationJobs");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "DesiredCareers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Knowledges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "JobSeekers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "JobSeekers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "JobApplications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "JobApplications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Experiences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "DesiredLocationJobs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "DesiredCareers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

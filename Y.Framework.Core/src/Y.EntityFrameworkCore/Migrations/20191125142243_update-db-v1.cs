﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatedbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YearsOfExperience",
                table: "Experiences",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YearsOfExperience",
                table: "Experiences",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

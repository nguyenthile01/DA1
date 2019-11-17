using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatetablev12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "JobSeekers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "JobSeekers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatetablev9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Careers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Careers",
                newName: "name");
        }
    }
}

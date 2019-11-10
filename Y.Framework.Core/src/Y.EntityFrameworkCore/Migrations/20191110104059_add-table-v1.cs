using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class addtablev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Cities",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "name");
        }
    }
}

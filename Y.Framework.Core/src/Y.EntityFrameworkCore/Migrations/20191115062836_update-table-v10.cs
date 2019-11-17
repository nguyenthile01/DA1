using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatetablev10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesiredCareers_Careers_CareerId",
                table: "DesiredCareers");

            migrationBuilder.RenameColumn(
                name: "CareerId",
                table: "DesiredCareers",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DesiredCareers_CareerId",
                table: "DesiredCareers",
                newName: "IX_DesiredCareers_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "avtarUrl",
                table: "JobSeekers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DesiredCareers_JobCategories_CategoryId",
                table: "DesiredCareers",
                column: "CategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesiredCareers_JobCategories_CategoryId",
                table: "DesiredCareers");

            migrationBuilder.DropColumn(
                name: "avtarUrl",
                table: "JobSeekers");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "DesiredCareers",
                newName: "CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_DesiredCareers_CategoryId",
                table: "DesiredCareers",
                newName: "IX_DesiredCareers_CareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesiredCareers_Careers_CareerId",
                table: "DesiredCareers",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

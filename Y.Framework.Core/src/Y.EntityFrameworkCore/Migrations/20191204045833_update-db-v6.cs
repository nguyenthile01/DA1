using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatedbv6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_CategoryId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Jobs",
                newName: "JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs",
                newName: "IX_Jobs_JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobCategoryId",
                table: "Jobs",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_JobCategoryId",
                table: "Jobs",
                newName: "IX_Jobs_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_CategoryId",
                table: "Jobs",
                column: "CategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

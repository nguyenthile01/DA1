using Microsoft.EntityFrameworkCore.Migrations;

namespace Y.Migrations
{
    public partial class updatetablev5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekers_Experiences_ExperienceId",
                table: "JobSeekers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekers_Knowledges_KnowledgeId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_ExperienceId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_KnowledgeId",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "KnowledgeId",
                table: "JobSeekers");

            migrationBuilder.AddColumn<int>(
                name: "JobSeekerId",
                table: "Knowledges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobSeekerId",
                table: "Experiences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Knowledges_JobSeekerId",
                table: "Knowledges",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_JobSeekerId",
                table: "Experiences",
                column: "JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_JobSeekers_JobSeekerId",
                table: "Experiences",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knowledges_JobSeekers_JobSeekerId",
                table: "Knowledges",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_JobSeekers_JobSeekerId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Knowledges_JobSeekers_JobSeekerId",
                table: "Knowledges");

            migrationBuilder.DropIndex(
                name: "IX_Knowledges_JobSeekerId",
                table: "Knowledges");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_JobSeekerId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Knowledges");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "JobSeekers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KnowledgeId",
                table: "JobSeekers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_ExperienceId",
                table: "JobSeekers",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_KnowledgeId",
                table: "JobSeekers",
                column: "KnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekers_Experiences_ExperienceId",
                table: "JobSeekers",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekers_Knowledges_KnowledgeId",
                table: "JobSeekers",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

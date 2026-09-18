using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class RestructureProjectRelations_AddMonitoringReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_closure_project_sanction_ProjectSanctionId",
                table: "project_closure");

            migrationBuilder.DropForeignKey(
                name: "FK_project_sanction_pre_project_PreProjectId",
                table: "project_sanction");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_project_closure_ProjectClosureId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_project_sanction_ProjectSanctionId",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_ProjectClosureId",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_ProjectSanctionId",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_project_sanction_PreProjectId",
                table: "project_sanction");

            migrationBuilder.DropIndex(
                name: "IX_project_closure_ProjectSanctionId",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "ProjectClosureId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "KickoffBrief",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "KickoffDate",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "KickoffMomFile",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "KickoffPptFileName",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "PmrcBrief",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "PmrcDate",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "PmrcMomFile",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "PmrcPptFileName",
                table: "project_sanction");

            migrationBuilder.RenameColumn(
                name: "PreProjectId",
                table: "project_sanction",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectSanctionId",
                table: "project_closure",
                newName: "ProjectId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "pre_project",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MonitoringReview",
                columns: table => new
                {
                    MonitoringReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    KickoffBrief = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KickoffDate = table.Column<DateTime>(type: "date", nullable: true),
                    KickoffPptFileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KickoffMomFile = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PmrcBrief = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PmrcDate = table.Column<DateTime>(type: "date", nullable: true),
                    PmrcPptFileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PmrcMomFile = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringReview", x => x.MonitoringReviewId);
                    table.ForeignKey(
                        name: "FK_MonitoringReview_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_project_sanction_ProjectId",
                table: "project_sanction",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_closure_ProjectId",
                table: "project_closure",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringReview_ProjectId",
                table: "MonitoringReview",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_project_closure_projects_ProjectId",
                table: "project_closure",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_sanction_projects_ProjectId",
                table: "project_sanction",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_closure_projects_ProjectId",
                table: "project_closure");

            migrationBuilder.DropForeignKey(
                name: "FK_project_sanction_projects_ProjectId",
                table: "project_sanction");

            migrationBuilder.DropTable(
                name: "MonitoringReview");

            migrationBuilder.DropIndex(
                name: "IX_project_sanction_ProjectId",
                table: "project_sanction");

            migrationBuilder.DropIndex(
                name: "IX_project_closure_ProjectId",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "pre_project");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "project_sanction",
                newName: "PreProjectId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "project_closure",
                newName: "ProjectSanctionId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectClosureId",
                table: "projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectSanctionId",
                table: "projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KickoffBrief",
                table: "project_sanction",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "KickoffDate",
                table: "project_sanction",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KickoffMomFile",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "KickoffPptFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PmrcBrief",
                table: "project_sanction",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "PmrcDate",
                table: "project_sanction",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PmrcMomFile",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PmrcPptFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectClosureId",
                table: "projects",
                column: "ProjectClosureId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectSanctionId",
                table: "projects",
                column: "ProjectSanctionId");

            migrationBuilder.CreateIndex(
                name: "IX_project_sanction_PreProjectId",
                table: "project_sanction",
                column: "PreProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_closure_ProjectSanctionId",
                table: "project_closure",
                column: "ProjectSanctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_closure_project_sanction_ProjectSanctionId",
                table: "project_closure",
                column: "ProjectSanctionId",
                principalTable: "project_sanction",
                principalColumn: "ProjectSanctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_sanction_pre_project_PreProjectId",
                table: "project_sanction",
                column: "PreProjectId",
                principalTable: "pre_project",
                principalColumn: "PreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_project_closure_ProjectClosureId",
                table: "projects",
                column: "ProjectClosureId",
                principalTable: "project_closure",
                principalColumn: "ProjectClosureId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_project_sanction_ProjectSanctionId",
                table: "projects",
                column: "ProjectSanctionId",
                principalTable: "project_sanction",
                principalColumn: "ProjectSanctionId");
        }
    }
}

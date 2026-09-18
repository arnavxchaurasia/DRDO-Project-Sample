using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMonitoringReviewToLongBlob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringReview_projects_ProjectId",
                table: "MonitoringReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitoringReview",
                table: "MonitoringReview");

            migrationBuilder.RenameTable(
                name: "MonitoringReview",
                newName: "monitoringreview");

            migrationBuilder.RenameIndex(
                name: "IX_MonitoringReview_ProjectId",
                table: "monitoringreview",
                newName: "IX_monitoringreview_ProjectId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PmrcPptFileName",
                table: "monitoringreview",
                type: "LONGBLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PmrcMomFile",
                table: "monitoringreview",
                type: "LONGBLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "KickoffPptFileName",
                table: "monitoringreview",
                type: "LONGBLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "KickoffMomFile",
                table: "monitoringreview",
                type: "LONGBLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_monitoringreview",
                table: "monitoringreview",
                column: "MonitoringReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_monitoringreview_projects_ProjectId",
                table: "monitoringreview",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monitoringreview_projects_ProjectId",
                table: "monitoringreview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_monitoringreview",
                table: "monitoringreview");

            migrationBuilder.RenameTable(
                name: "monitoringreview",
                newName: "MonitoringReview");

            migrationBuilder.RenameIndex(
                name: "IX_monitoringreview_ProjectId",
                table: "MonitoringReview",
                newName: "IX_MonitoringReview_ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "PmrcPptFileName",
                table: "MonitoringReview",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "LONGBLOB",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PmrcMomFile",
                table: "MonitoringReview",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "LONGBLOB",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "KickoffPptFileName",
                table: "MonitoringReview",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "LONGBLOB",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "KickoffMomFile",
                table: "MonitoringReview",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "LONGBLOB",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitoringReview",
                table: "MonitoringReview",
                column: "MonitoringReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringReview_projects_ProjectId",
                table: "MonitoringReview",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

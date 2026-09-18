using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSanctionBlobFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrigendumFileName",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "DeliverableFileName",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "EbmPptFileName",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "SanctionLetterFileName",
                table: "project_sanction");

            migrationBuilder.AlterColumn<byte[]>(
                name: "EbmMomFile",
                table: "project_sanction",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "CorrigendumFile",
                table: "project_sanction",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DeliverableFile",
                table: "project_sanction",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "EbmPptFile",
                table: "project_sanction",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SanctionLetterFile",
                table: "project_sanction",
                type: "longblob",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrigendumFile",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "DeliverableFile",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "EbmPptFile",
                table: "project_sanction");

            migrationBuilder.DropColumn(
                name: "SanctionLetterFile",
                table: "project_sanction");

            migrationBuilder.AlterColumn<string>(
                name: "EbmMomFile",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CorrigendumFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeliverableFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EbmPptFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SanctionLetterFileName",
                table: "project_sanction",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

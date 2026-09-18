using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectClosureFileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcFileName",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "ClFileName",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "IdcmFileName",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "IdcmMomFileName",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "TcrFileName",
                table: "project_closure");

            migrationBuilder.AddColumn<byte[]>(
                name: "AcFile",
                table: "project_closure",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ClFile",
                table: "project_closure",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "IdcmFile",
                table: "project_closure",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "IdcmMomFile",
                table: "project_closure",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TcrFile",
                table: "project_closure",
                type: "longblob",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcFile",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "ClFile",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "IdcmFile",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "IdcmMomFile",
                table: "project_closure");

            migrationBuilder.DropColumn(
                name: "TcrFile",
                table: "project_closure");

            migrationBuilder.AddColumn<string>(
                name: "AcFileName",
                table: "project_closure",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClFileName",
                table: "project_closure",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdcmFileName",
                table: "project_closure",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdcmMomFileName",
                table: "project_closure",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TcrFileName",
                table: "project_closure",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

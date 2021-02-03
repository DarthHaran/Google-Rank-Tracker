using Microsoft.EntityFrameworkCore.Migrations;

namespace GRT.Data.Migrations
{
    public partial class KeywordUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Keywords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityBase64",
                table: "Keywords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Keywords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleHost",
                table: "Keywords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Keywords",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "CityBase64",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "GoogleHost",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Keywords");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GRT.Data.Migrations
{
    public partial class B64propertyRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityBase64",
                table: "Keywords");

            migrationBuilder.AlterColumn<string>(
                name: "KeywordName",
                table: "Keywords",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeywordName",
                table: "Keywords",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CityBase64",
                table: "Keywords",
                type: "TEXT",
                nullable: true);
        }
    }
}

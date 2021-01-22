using Microsoft.EntityFrameworkCore.Migrations;

namespace GRT.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_Projects_ProjectId",
                table: "Keywords");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Keywords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_Projects_ProjectId",
                table: "Keywords",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_Projects_ProjectId",
                table: "Keywords");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Keywords",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_Projects_ProjectId",
                table: "Keywords",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

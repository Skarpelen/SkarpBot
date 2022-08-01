using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkarpBot.Data.Migrations
{
    public partial class MinorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GuildsId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "GuildsId",
                table: "Users",
                type: "bigint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

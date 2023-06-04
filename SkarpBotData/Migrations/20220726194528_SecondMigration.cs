using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkarpBot.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Guilds_GuildsId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Users_GuildsId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<ulong>(
                name: "GuildsId",
                table: "User",
                type: "bigint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GuildsId1",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Grenades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GuildsId1",
                table: "User",
                column: "GuildsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Guilds_GuildsId1",
                table: "User",
                column: "GuildsId1",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Guilds_GuildsId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Users_GuildsId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "GuildsId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Grenades");

            migrationBuilder.AlterColumn<int>(
                name: "GuildsId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GuildsId",
                table: "User",
                column: "GuildsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Guilds_GuildsId",
                table: "User",
                column: "GuildsId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

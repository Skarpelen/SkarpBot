using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkarpBot.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grenades_Users_UsersId",
                table: "Grenades");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Users_UsersId",
                table: "Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Guilds_GuildsId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Users_UsersId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_UsersId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Users_GuildsId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Status_UsersId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Grenades_UsersId",
                table: "Grenades");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "GuildsId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Grenades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuildsId1",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Grenades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_UsersId",
                table: "Weapons",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GuildsId1",
                table: "Users",
                column: "GuildsId1");

            migrationBuilder.CreateIndex(
                name: "IX_Status_UsersId",
                table: "Status",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Grenades_UsersId",
                table: "Grenades",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grenades_Users_UsersId",
                table: "Grenades",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Users_UsersId",
                table: "Status",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Guilds_GuildsId1",
                table: "Users",
                column: "GuildsId1",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Users_UsersId",
                table: "Weapons",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

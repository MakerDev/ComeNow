using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeNow.Persistance.Migrations
{
    public partial class PushAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receivers_PushCommands_PushCommandId",
                table: "Receivers");

            migrationBuilder.DropIndex(
                name: "IX_Receivers_PushCommandId",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "PushCommandId",
                table: "Receivers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PushCommands",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PushCommands_AppUserId",
                table: "PushCommands",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PushCommands_AspNetUsers_AppUserId",
                table: "PushCommands",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushCommands_AspNetUsers_AppUserId",
                table: "PushCommands");

            migrationBuilder.DropIndex(
                name: "IX_PushCommands_AppUserId",
                table: "PushCommands");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PushCommands");

            migrationBuilder.AddColumn<int>(
                name: "PushCommandId",
                table: "Receivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_PushCommandId",
                table: "Receivers",
                column: "PushCommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receivers_PushCommands_PushCommandId",
                table: "Receivers",
                column: "PushCommandId",
                principalTable: "PushCommands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

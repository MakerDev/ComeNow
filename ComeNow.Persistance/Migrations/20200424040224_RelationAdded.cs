using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeNow.Persistance.Migrations
{
    public partial class RelationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Receivers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_OwnerId",
                table: "Receivers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receivers_AspNetUsers_OwnerId",
                table: "Receivers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receivers_AspNetUsers_OwnerId",
                table: "Receivers");

            migrationBuilder.DropIndex(
                name: "IX_Receivers_OwnerId",
                table: "Receivers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Receivers");
        }
    }
}

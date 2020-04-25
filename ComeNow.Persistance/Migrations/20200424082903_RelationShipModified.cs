using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeNow.Persistance.Migrations
{
    public partial class RelationShipModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandReceiver",
                columns: table => new
                {
                    CommandId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandReceiver", x => new { x.CommandId, x.ReceiverId });
                    table.ForeignKey(
                        name: "FK_CommandReceiver_PushCommands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "PushCommands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandReceiver_Receivers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandReceiver_ReceiverId",
                table: "CommandReceiver",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandReceiver");
        }
    }
}

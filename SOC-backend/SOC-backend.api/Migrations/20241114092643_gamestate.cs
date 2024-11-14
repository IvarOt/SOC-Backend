using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class gamestate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopCard",
                columns: table => new
                {
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCard", x => new { x.ShopId, x.CardId });
                    table.ForeignKey(
                        name: "FK_ShopCard_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopCard_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PlayersTurn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameStateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opponent_GameState_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opponent_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpponentCard",
                columns: table => new
                {
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    IsOffence = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpponentCard", x => new { x.OpponentId, x.CardId });
                    table.ForeignKey(
                        name: "FK_OpponentCard_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpponentCard_Opponent_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "Opponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameState_PlayerId",
                table: "GameState",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Opponent_GameStateId",
                table: "Opponent",
                column: "GameStateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opponent_ShopId",
                table: "Opponent",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OpponentCard_CardId",
                table: "OpponentCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCard_CardId",
                table: "ShopCard",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameState_Opponent_PlayerId",
                table: "GameState",
                column: "PlayerId",
                principalTable: "Opponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameState_Opponent_PlayerId",
                table: "GameState");

            migrationBuilder.DropTable(
                name: "OpponentCard");

            migrationBuilder.DropTable(
                name: "ShopCard");

            migrationBuilder.DropTable(
                name: "Opponent");

            migrationBuilder.DropTable(
                name: "GameState");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Card");
        }
    }
}

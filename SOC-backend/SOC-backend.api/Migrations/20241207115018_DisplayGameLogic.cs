using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class DisplayGameLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayersTurn",
                table: "GameState");

            migrationBuilder.AddColumn<int>(
                name: "DMG",
                table: "OpponentCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "OpponentCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionIndex",
                table: "OpponentCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurnNumber",
                table: "GameState",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardFight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameStateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardFight_GameState_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FightCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HP = table.Column<int>(type: "int", nullable: false),
                    DMG = table.Column<int>(type: "int", nullable: false),
                    CardFightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightCard_CardFight_CardFightId",
                        column: x => x.CardFightId,
                        principalTable: "CardFight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardFight_GameStateId",
                table: "CardFight",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_FightCard_CardFightId",
                table: "FightCard",
                column: "CardFightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FightCard");

            migrationBuilder.DropTable(
                name: "CardFight");

            migrationBuilder.DropColumn(
                name: "DMG",
                table: "OpponentCard");

            migrationBuilder.DropColumn(
                name: "HP",
                table: "OpponentCard");

            migrationBuilder.DropColumn(
                name: "PositionIndex",
                table: "OpponentCard");

            migrationBuilder.DropColumn(
                name: "TurnNumber",
                table: "GameState");

            migrationBuilder.AddColumn<bool>(
                name: "PlayersTurn",
                table: "GameState",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

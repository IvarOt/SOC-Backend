using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class fightCascadeDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardFight_GameState_GameStateId",
                table: "CardFight");

            migrationBuilder.AlterColumn<int>(
                name: "GameStateId",
                table: "CardFight",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFight_GameState_GameStateId",
                table: "CardFight",
                column: "GameStateId",
                principalTable: "GameState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardFight_GameState_GameStateId",
                table: "CardFight");

            migrationBuilder.AlterColumn<int>(
                name: "GameStateId",
                table: "CardFight",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CardFight_GameState_GameStateId",
                table: "CardFight",
                column: "GameStateId",
                principalTable: "GameState",
                principalColumn: "Id");
        }
    }
}

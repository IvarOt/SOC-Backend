using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerToFinishedMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "finishedMatch",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_finishedMatch_PlayerId",
                table: "finishedMatch",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_finishedMatch_Player_PlayerId",
                table: "finishedMatch",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_finishedMatch_Player_PlayerId",
                table: "finishedMatch");

            migrationBuilder.DropIndex(
                name: "IX_finishedMatch_PlayerId",
                table: "finishedMatch");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "finishedMatch");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class fixingOpponentCardEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OpponentCard",
                table: "OpponentCard");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OpponentCard",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpponentCard",
                table: "OpponentCard",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpponentCard_OpponentId",
                table: "OpponentCard",
                column: "OpponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OpponentCard",
                table: "OpponentCard");

            migrationBuilder.DropIndex(
                name: "IX_OpponentCard_OpponentId",
                table: "OpponentCard");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OpponentCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpponentCard",
                table: "OpponentCard",
                columns: new[] { "OpponentId", "CardId" });
        }
    }
}

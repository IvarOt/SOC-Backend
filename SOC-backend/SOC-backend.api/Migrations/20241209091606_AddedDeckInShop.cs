using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeckInShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardShop",
                columns: table => new
                {
                    AvailableCardsId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardShop", x => new { x.AvailableCardsId, x.ShopId });
                    table.ForeignKey(
                        name: "FK_CardShop_Card_AvailableCardsId",
                        column: x => x.AvailableCardsId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardShop_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardShop_ShopId",
                table: "CardShop",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardShop");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShopCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPurchased",
                table: "ShopCard",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "ShopCard");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOC_backend.api.Migrations
{
    /// <inheritdoc />
    public partial class addCost : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Card");
        }
    }
}

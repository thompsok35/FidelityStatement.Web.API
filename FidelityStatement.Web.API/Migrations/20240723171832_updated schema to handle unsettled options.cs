using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedschematohandleunsettledoptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnsettledPremium",
                table: "Positions",
                type: "decimal(19,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSettled",
                table: "PositionOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnsettledPremium",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "isSettled",
                table: "PositionOptions");
        }
    }
}

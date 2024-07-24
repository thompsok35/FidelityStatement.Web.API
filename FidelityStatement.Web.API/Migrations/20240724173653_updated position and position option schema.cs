using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedpositionandpositionoptionschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnsettledOptions",
                table: "Positions",
                newName: "UnsettledOptionTrades");

            migrationBuilder.AddColumn<decimal>(
                name: "AdjustedCostBasis",
                table: "Positions",
                type: "decimal(19,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockID",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalOptionTrades",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "PositionOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustedCostBasis",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "StockID",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TotalOptionTrades",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "UnsettledOptionTrades",
                table: "Positions",
                newName: "UnsettledOptions");

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "PositionOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

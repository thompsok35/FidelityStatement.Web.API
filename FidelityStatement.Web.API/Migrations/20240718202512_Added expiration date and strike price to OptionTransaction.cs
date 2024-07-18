using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedexpirationdateandstrikepricetoOptionTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "OptionTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "StrikePrice",
                table: "OptionTransaction",
                type: "decimal(19,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "OptionTransaction");

            migrationBuilder.DropColumn(
                name: "StrikePrice",
                table: "OptionTransaction");
        }
    }
}

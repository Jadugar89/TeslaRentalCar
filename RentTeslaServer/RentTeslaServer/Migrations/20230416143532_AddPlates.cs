using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentTeslaServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPlates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Plates",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plates",
                table: "Cars");
        }
    }
}

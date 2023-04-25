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
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Plates",
                table: "Cars",
                column: "Plates",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_Plates",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Plates",
                table: "Cars");
        }
    }
}

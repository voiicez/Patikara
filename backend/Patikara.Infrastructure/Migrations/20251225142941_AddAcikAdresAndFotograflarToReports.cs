using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patikara.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcikAdresAndFotograflarToReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcikAdres",
                table: "Reports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fotograflar",
                table: "Reports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcikAdres",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Fotograflar",
                table: "Reports");
        }
    }
}

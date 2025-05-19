using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Achi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class welcomeISO2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISO2",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISO2",
                table: "Countries");
        }
    }
}

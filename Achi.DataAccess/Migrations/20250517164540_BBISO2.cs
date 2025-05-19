using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Achi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BBISO2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISO2",
                table: "Countries",
                newName: "ISO3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISO3",
                table: "Countries",
                newName: "ISO2");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityWebApi.Migrations
{
    /// <inheritdoc />
    public partial class YeniDegisiklik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHast",
                table: "UserEntities",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "UserEntities",
                newName: "PasswordHast");
        }
    }
}

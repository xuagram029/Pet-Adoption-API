using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet_Adoption_API.Migrations
{
    /// <inheritdoc />
    public partial class Removedredundantconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shelters",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_Name",
                table: "Shelters",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shelters_Name",
                table: "Shelters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shelters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIQuim.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToMateriales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Materiales",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Materiales");
        }
    }
}

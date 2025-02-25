using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Youth_Innovation_System.Repository.Identity.Migrations
{
    /// <inheritdoc />
    public partial class DeletedipAddressfromusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ipAddress",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ipAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

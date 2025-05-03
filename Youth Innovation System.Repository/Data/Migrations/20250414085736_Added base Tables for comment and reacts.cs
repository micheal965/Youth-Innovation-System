using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Youth_Innovation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedbaseTablesforcommentandreacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createOn",
                table: "CommentReplies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createOn",
                table: "CommentReplies");
        }
    }
}

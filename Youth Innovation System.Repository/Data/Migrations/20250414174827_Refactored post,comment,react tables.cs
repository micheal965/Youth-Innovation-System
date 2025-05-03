using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Youth_Innovation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactoredpostcommentreacttables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reacts_Posts_postId",
                table: "Reacts");

            migrationBuilder.DropTable(
                name: "CommentReactions");

            migrationBuilder.DropTable(
                name: "CommentReplies");

            migrationBuilder.RenameColumn(
                name: "postId",
                table: "Reacts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Reacts_postId",
                table: "Reacts",
                newName: "IX_Reacts_PostId");

            migrationBuilder.AlterColumn<string>(
                name: "reactionType",
                table: "Reacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Reacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_CommentId",
                table: "Reacts",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reacts_Comments_CommentId",
                table: "Reacts",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacts_Posts_PostId",
                table: "Reacts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacts_Comments_CommentId",
                table: "Reacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacts_Posts_PostId",
                table: "Reacts");

            migrationBuilder.DropIndex(
                name: "IX_Reacts_CommentId",
                table: "Reacts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Reacts");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Reacts",
                newName: "postId");

            migrationBuilder.RenameIndex(
                name: "IX_Reacts_PostId",
                table: "Reacts",
                newName: "IX_Reacts_postId");

            migrationBuilder.AlterColumn<string>(
                name: "reactionType",
                table: "Reacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "postId",
                table: "Reacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CommentReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReactions_Comments_commentId",
                        column: x => x.commentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReplies_Comments_commentId",
                        column: x => x.commentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_commentId",
                table: "CommentReactions",
                column: "commentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_commentId",
                table: "CommentReplies",
                column: "commentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reacts_Posts_postId",
                table: "Reacts",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

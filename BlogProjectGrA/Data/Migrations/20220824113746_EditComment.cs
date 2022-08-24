using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjectGrA.Data.Migrations
{
    public partial class EditComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Comments_CommentId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CommentId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostsId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostsId",
                table: "Comments",
                column: "PostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostsId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostsId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommentId",
                table: "Posts",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Comments_CommentId",
                table: "Posts",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}

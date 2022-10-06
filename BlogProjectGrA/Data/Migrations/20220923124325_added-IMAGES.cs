using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjectGrA.Data.Migrations
{
    public partial class addedIMAGES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tags_Blogs_BlogId",
            //    table: "Tags");

            //migrationBuilder.DropIndex(
            //    name: "IX_Tags_BlogId",
            //    table: "Tags");

            //migrationBuilder.DropColumn(
            //    name: "BlogId",
            //    table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "PostsId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ImagesVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogImagesVM",
                columns: table => new
                {
                    BlogsId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImagesVM", x => new { x.BlogsId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_BlogImagesVM_Blogs_BlogsId",
                        column: x => x.BlogsId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogImagesVM_ImagesVM_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ImagesVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagesVMPost",
                columns: table => new
                {
                    ImagesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesVMPost", x => new { x.ImagesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_ImagesVMPost_ImagesVM_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ImagesVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesVMPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogImagesVM_ImagesId",
                table: "BlogImagesVM",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesVMPost_PostsId",
                table: "ImagesVMPost",
                column: "PostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "BlogImagesVM");

            migrationBuilder.DropTable(
                name: "ImagesVMPost");

            migrationBuilder.DropTable(
                name: "ImagesVM");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostsId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogId",
                table: "Tags",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostsId",
                table: "Comments",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Blogs_BlogId",
                table: "Tags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}

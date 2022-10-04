using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjectGrA.Data.Migrations
{
    public partial class addpropertyimsgrUrlinblogmodelcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImagesVM");

            migrationBuilder.DropTable(
                name: "ImagesVMPost");

            migrationBuilder.DropTable(
                name: "ImagesVM");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Blogs");

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
        }
    }
}

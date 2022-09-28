using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: true),
                    QuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Message_PostId",
                        column: x => x.PostId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Message_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDislikedPosts",
                columns: table => new
                {
                    DislikedPostsId = table.Column<long>(type: "bigint", nullable: false),
                    UserDislikesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDislikedPosts", x => new { x.DislikedPostsId, x.UserDislikesId });
                    table.ForeignKey(
                        name: "FK_UsersDislikedPosts_Message_DislikedPostsId",
                        column: x => x.DislikedPostsId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersDislikedPosts_Users_UserDislikesId",
                        column: x => x.UserDislikesId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersLikedPosts",
                columns: table => new
                {
                    LikedPostsId = table.Column<long>(type: "bigint", nullable: false),
                    UserLikesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedPosts", x => new { x.LikedPostsId, x.UserLikesId });
                    table.ForeignKey(
                        name: "FK_UsersLikedPosts_Message_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersLikedPosts_Users_UserLikesId",
                        column: x => x.UserLikesId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_AuthorId",
                table: "Message",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_PostId",
                table: "Message",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_QuestionId",
                table: "Message",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDislikedPosts_UserDislikesId",
                table: "UsersDislikedPosts",
                column: "UserDislikesId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedPosts_UserLikesId",
                table: "UsersLikedPosts",
                column: "UserLikesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDislikedPosts");

            migrationBuilder.DropTable(
                name: "UsersLikedPosts");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

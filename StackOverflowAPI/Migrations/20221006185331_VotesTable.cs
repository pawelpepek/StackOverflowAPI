using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowAPI.Migrations
{
    public partial class VotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDislikedPosts");

            migrationBuilder.DropTable(
                name: "UsersLikedPosts");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Message_PostId",
                        column: x => x.PostId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PostId",
                table: "Votes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

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
                name: "IX_UsersDislikedPosts_UserDislikesId",
                table: "UsersDislikedPosts",
                column: "UserDislikesId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedPosts_UserLikesId",
                table: "UsersLikedPosts",
                column: "UserLikesId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace McvMovie.Migrations
{
    /// <inheritdoc />
    public partial class actorsInMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Star",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ActorId",
                table: "Star",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Star_ActorId",
                table: "Star",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Star_MovieId",
                table: "Star",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Star_Actor_ActorId",
                table: "Star",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Star_Movie_MovieId",
                table: "Star",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Star_Actor_ActorId",
                table: "Star");

            migrationBuilder.DropForeignKey(
                name: "FK_Star_Movie_MovieId",
                table: "Star");

            migrationBuilder.DropIndex(
                name: "IX_Star_ActorId",
                table: "Star");

            migrationBuilder.DropIndex(
                name: "IX_Star_MovieId",
                table: "Star");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Star",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActorId",
                table: "Star",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoviesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actor_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");
        }
    }
}

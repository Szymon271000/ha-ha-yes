using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    SerieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerieName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SerieId);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    CredentialsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.CredentialsID);
                });

            migrationBuilder.CreateTable(
                name: "GenreSerie",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    SerieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreSerie", x => new { x.GenreId, x.SerieId });
                    table.ForeignKey(
                        name: "FK_GenreSerie_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreSerie_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonNumber = table.Column<int>(type: "int", nullable: true),
                    SerieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.SeasonId);
                    table.ForeignKey(
                        name: "FK_Seasons_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserCredentials_CredentialsID",
                        column: x => x.CredentialsID,
                        principalTable: "UserCredentials",
                        principalColumn: "CredentialsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodeNumber = table.Column<int>(type: "int", nullable: true),
                    EpisodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.EpisodeId);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActorEpisode",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEpisode", x => new { x.ActorId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_ActorEpisode_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorEpisode_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "EpisodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "ActorId", "ActorName" },
                values: new object[,]
                {
                    { 1, "Robert De Niro" },
                    { 2, "Jack Nicholson" },
                    { 3, "Marlon Brando" },
                    { 4, "Denzel Washington" },
                    { 5, "Katharine Hepburn" },
                    { 6, "Humphrey Bogart" },
                    { 7, "Meryl Streep" },
                    { 8, "Daniel Day-Lewis" },
                    { 9, "Sidney Poitier" },
                    { 10, "Clark Gable" },
                    { 11, "Ingrid Bergman" },
                    { 12, "Tom Hanks" },
                    { 13, "Elizabeth Taylor" },
                    { 14, "Bette Davis" },
                    { 15, "Gregory Peck" },
                    { 16, "Leonardo DiCaprio" },
                    { 17, "Cate Blanchett" },
                    { 18, "Audrey Hepburn" },
                    { 19, "Spencer Tracy" },
                    { 20, "Kate Winslet" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Comedy" },
                    { 2, "Horror" },
                    { 3, "Action" },
                    { 4, "Musical" },
                    { 5, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "SerieId", "SerieName" },
                values: new object[,]
                {
                    { 1, "Planet Earth II" },
                    { 2, "Breaking Bad" },
                    { 3, "Planet Earth" },
                    { 4, "Band of Brothers" },
                    { 6, "The Wire" },
                    { 7, "Blue Planet II" },
                    { 8, "Avatar: The Last Airbender" },
                    { 9, "Cosmos: A Spacetime Odyssey" },
                    { 10, "The Sopranos" }
                });

            migrationBuilder.InsertData(
                table: "GenreSerie",
                columns: new[] { "GenreId", "SerieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 5, 10 }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "SeasonId", "SeasonNumber", "SerieId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 2 },
                    { 5, 1, 3 },
                    { 6, 2, 3 },
                    { 7, 1, 4 },
                    { 8, 2, 4 },
                    { 11, 1, 6 },
                    { 12, 2, 6 },
                    { 13, 1, 7 },
                    { 14, 2, 7 },
                    { 15, 1, 8 },
                    { 16, 2, 8 },
                    { 17, 1, 9 },
                    { 18, 2, 9 },
                    { 19, 1, 10 },
                    { 20, 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "Episodes",
                columns: new[] { "EpisodeId", "EpisodeName", "EpisodeNumber", "SeasonId" },
                values: new object[,]
                {
                    { 1, "volume", 1, 1 },
                    { 2, "association", 2, 1 },
                    { 3, "promotion", 1, 2 },
                    { 4, "interaction", 2, 2 },
                    { 5, "camera", 1, 3 },
                    { 6, "knowledge", 2, 3 },
                    { 7, "cookie", 1, 4 },
                    { 8, "employer", 2, 4 },
                    { 9, "revenue", 1, 5 },
                    { 10, "selection", 2, 5 },
                    { 11, "piano", 1, 6 },
                    { 12, "airport", 2, 6 },
                    { 13, "news", 1, 7 },
                    { 14, "friendship", 2, 7 },
                    { 15, "intention", 1, 8 },
                    { 16, "understanding", 2, 8 },
                    { 21, "guitar", 1, 11 },
                    { 22, "initiative", 2, 11 },
                    { 23, "relationship", 1, 12 },
                    { 24, "safety", 2, 12 },
                    { 25, "data", 1, 13 },
                    { 26, "ad", 2, 13 },
                    { 27, "bath", 1, 14 },
                    { 28, "platform", 2, 14 },
                    { 29, "hall", 1, 15 },
                    { 30, "quality", 2, 15 },
                    { 31, "girlfriend", 1, 16 },
                    { 32, "dirt", 2, 16 },
                    { 33, "complaint", 1, 17 },
                    { 34, "examination", 2, 17 },
                    { 35, "assignment", 1, 18 },
                    { 36, "chocolate", 2, 18 },
                    { 37, "beer", 1, 19 },
                    { 38, "passion", 2, 19 },
                    { 39, "night", 1, 20 },
                    { 40, "reception", 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "ActorEpisode",
                columns: new[] { "ActorId", "EpisodeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 5 },
                    { 1, 9 },
                    { 1, 13 },
                    { 1, 21 },
                    { 1, 24 },
                    { 1, 29 },
                    { 1, 34 },
                    { 2, 1 },
                    { 2, 5 },
                    { 2, 9 },
                    { 2, 13 },
                    { 2, 21 },
                    { 2, 25 },
                    { 2, 29 },
                    { 2, 34 },
                    { 3, 1 },
                    { 3, 5 },
                    { 3, 9 },
                    { 3, 13 },
                    { 3, 21 },
                    { 3, 25 },
                    { 3, 29 },
                    { 3, 34 },
                    { 4, 1 },
                    { 4, 5 },
                    { 4, 9 },
                    { 4, 13 },
                    { 4, 21 },
                    { 4, 25 },
                    { 4, 29 },
                    { 4, 34 },
                    { 5, 1 },
                    { 5, 5 },
                    { 5, 9 },
                    { 5, 13 },
                    { 5, 21 },
                    { 5, 25 },
                    { 5, 29 },
                    { 5, 34 },
                    { 6, 2 },
                    { 6, 10 }
                });

            migrationBuilder.InsertData(
                table: "ActorEpisode",
                columns: new[] { "ActorId", "EpisodeId" },
                values: new object[,]
                {
                    { 6, 25 },
                    { 7, 2 },
                    { 7, 6 },
                    { 7, 10 },
                    { 7, 14 },
                    { 7, 22 },
                    { 7, 26 },
                    { 7, 30 },
                    { 7, 35 },
                    { 7, 38 },
                    { 8, 2 },
                    { 8, 6 },
                    { 8, 10 },
                    { 8, 14 },
                    { 8, 22 },
                    { 8, 26 },
                    { 8, 30 },
                    { 8, 35 },
                    { 8, 38 },
                    { 9, 2 },
                    { 9, 6 },
                    { 9, 10 },
                    { 9, 14 },
                    { 9, 22 },
                    { 9, 26 },
                    { 9, 30 },
                    { 9, 35 },
                    { 9, 38 },
                    { 10, 2 },
                    { 10, 6 },
                    { 10, 10 },
                    { 10, 14 },
                    { 10, 22 },
                    { 10, 26 },
                    { 10, 31 },
                    { 10, 35 },
                    { 10, 38 },
                    { 11, 3 },
                    { 11, 6 },
                    { 11, 11 },
                    { 11, 14 },
                    { 11, 22 }
                });

            migrationBuilder.InsertData(
                table: "ActorEpisode",
                columns: new[] { "ActorId", "EpisodeId" },
                values: new object[,]
                {
                    { 11, 27 },
                    { 11, 31 },
                    { 11, 35 },
                    { 11, 38 },
                    { 12, 3 },
                    { 12, 7 },
                    { 12, 11 },
                    { 12, 15 },
                    { 12, 23 },
                    { 12, 27 },
                    { 12, 32 },
                    { 12, 36 },
                    { 12, 39 },
                    { 13, 3 },
                    { 13, 7 },
                    { 13, 11 },
                    { 13, 15 },
                    { 13, 23 },
                    { 13, 27 },
                    { 13, 32 },
                    { 13, 36 },
                    { 13, 39 },
                    { 14, 3 },
                    { 14, 7 },
                    { 14, 11 },
                    { 14, 15 },
                    { 14, 23 },
                    { 14, 27 },
                    { 14, 32 },
                    { 14, 36 },
                    { 14, 39 },
                    { 15, 3 },
                    { 15, 7 },
                    { 15, 11 },
                    { 15, 15 },
                    { 15, 23 },
                    { 15, 27 },
                    { 15, 32 },
                    { 15, 36 },
                    { 15, 39 },
                    { 16, 4 },
                    { 16, 7 }
                });

            migrationBuilder.InsertData(
                table: "ActorEpisode",
                columns: new[] { "ActorId", "EpisodeId" },
                values: new object[,]
                {
                    { 16, 12 },
                    { 16, 15 },
                    { 16, 23 },
                    { 16, 28 },
                    { 16, 32 },
                    { 16, 36 },
                    { 16, 39 },
                    { 17, 4 },
                    { 17, 8 },
                    { 17, 12 },
                    { 17, 16 },
                    { 17, 24 },
                    { 17, 28 },
                    { 17, 33 },
                    { 17, 37 },
                    { 17, 40 },
                    { 18, 4 },
                    { 18, 8 },
                    { 18, 12 },
                    { 18, 16 },
                    { 18, 24 },
                    { 18, 28 },
                    { 18, 33 },
                    { 18, 37 },
                    { 18, 40 },
                    { 19, 4 },
                    { 19, 8 },
                    { 19, 12 },
                    { 19, 16 },
                    { 19, 24 },
                    { 19, 28 },
                    { 19, 33 },
                    { 19, 37 },
                    { 19, 40 },
                    { 20, 4 },
                    { 20, 8 },
                    { 20, 12 },
                    { 20, 16 },
                    { 20, 24 },
                    { 20, 28 },
                    { 20, 33 },
                    { 20, 37 }
                });

            migrationBuilder.InsertData(
                table: "ActorEpisode",
                columns: new[] { "ActorId", "EpisodeId" },
                values: new object[] { 20, 40 });

            migrationBuilder.CreateIndex(
                name: "IX_ActorEpisode_EpisodeId",
                table: "ActorEpisode",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreSerie_SerieId",
                table: "GenreSerie",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SerieId",
                table: "Seasons",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialsID",
                table: "Users",
                column: "CredentialsID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorEpisode");

            migrationBuilder.DropTable(
                name: "GenreSerie");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}

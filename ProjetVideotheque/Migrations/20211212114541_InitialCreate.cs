using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ProjetVideotheque.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrenomClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbFilmsLoues = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFilm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealisateurFilm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSortieFilm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbLocationsFilm = table.Column<int>(type: "int", nullable: false),
                    DisponibiliteFilm = table.Column<bool>(type: "bit", nullable: false),
                    CategorieFilm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrixParJour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DateDebutLocation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourLocation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenduFilm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_ClientId",
                table: "Location",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_FilmId",
                table: "Location",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}

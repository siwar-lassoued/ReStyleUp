using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReStyleUp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annonces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Prix = table.Column<float>(type: "REAL", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UtilisateurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annonces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Prix = table.Column<float>(type: "REAL", nullable: false),
                    Categorie = table.Column<int>(type: "INTEGER", nullable: false),
                    Etat = table.Column<int>(type: "INTEGER", nullable: false),
                    AnnonceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Annonces_AnnonceId",
                        column: x => x.AnnonceId,
                        principalTable: "Annonces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    AnnonceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Annonces_AnnonceId",
                        column: x => x.AnnonceId,
                        principalTable: "Annonces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticleCommande",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommandesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommande", x => new { x.ArticlesId, x.CommandesId });
                    table.ForeignKey(
                        name: "FK_ArticleCommande_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCommande = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MontantTotal = table.Column<float>(type: "REAL", nullable: false),
                    UtilisateurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    MotDePasse = table.Column<string>(type: "TEXT", nullable: false),
                    Adresse = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommandeId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    AnnonceId = table.Column<int>(type: "INTEGER", nullable: true),
                    AnnonceId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Annonces_AnnonceId1",
                        column: x => x.AnnonceId1,
                        principalTable: "Annonces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Commandes_CommandeId1",
                        column: x => x.CommandeId1,
                        principalTable: "Commandes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annonces_UtilisateurId",
                table: "Annonces",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommande_CommandesId",
                table: "ArticleCommande",
                column: "CommandesId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AnnonceId",
                table: "Articles",
                column: "AnnonceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_UtilisateurId",
                table: "Commandes",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnnonceId",
                table: "Images",
                column: "AnnonceId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_AnnonceId1",
                table: "Utilisateurs",
                column: "AnnonceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_CommandeId1",
                table: "Utilisateurs",
                column: "CommandeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_Utilisateurs_UtilisateurId",
                table: "Annonces",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCommande_Commandes_CommandesId",
                table: "ArticleCommande",
                column: "CommandesId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_UtilisateurId",
                table: "Commandes",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_Utilisateurs_UtilisateurId",
                table: "Annonces");

            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_UtilisateurId",
                table: "Commandes");

            migrationBuilder.DropTable(
                name: "ArticleCommande");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Annonces");

            migrationBuilder.DropTable(
                name: "Commandes");
        }
    }
}

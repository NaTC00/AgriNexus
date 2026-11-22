using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriNexus.Authorization.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Iniziale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permessi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codice = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permessi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codice = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Attivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuoliPermessi",
                columns: table => new
                {
                    PermessoId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuoloId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuoliPermessi", x => new { x.PermessoId, x.RuoloId });
                    table.ForeignKey(
                        name: "FK_RuoliPermessi_Permessi_PermessoId",
                        column: x => x.PermessoId,
                        principalTable: "Permessi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuoliPermessi_Ruoli_RuoloId",
                        column: x => x.RuoloId,
                        principalTable: "Ruoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssegnazioniRuolo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UtenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuoloId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoAmbito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValoreAmbito = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssegnazioniRuolo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssegnazioniRuolo_Ruoli_RuoloId",
                        column: x => x.RuoloId,
                        principalTable: "Ruoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssegnazioniRuolo_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssegnazioniRuolo_RuoloId",
                table: "AssegnazioniRuolo",
                column: "RuoloId");

            migrationBuilder.CreateIndex(
                name: "IX_AssegnazioniRuolo_UtenteId_RuoloId_TipoAmbito_ValoreAmbito",
                table: "AssegnazioniRuolo",
                columns: new[] { "UtenteId", "RuoloId", "TipoAmbito", "ValoreAmbito" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permessi_Codice",
                table: "Permessi",
                column: "Codice",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ruoli_Codice",
                table: "Ruoli",
                column: "Codice",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuoliPermessi_RuoloId",
                table: "RuoliPermessi",
                column: "RuoloId");

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Username",
                table: "Utenti",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssegnazioniRuolo");

            migrationBuilder.DropTable(
                name: "RuoliPermessi");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Permessi");

            migrationBuilder.DropTable(
                name: "Ruoli");
        }
    }
}

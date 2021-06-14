using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarea3.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moras",
                columns: table => new 
                {
                    MoraId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moras", x => x.MoraId);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    Visibilidad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "MorasDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoraId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrestamoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorasDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MorasDetalle_Moras_MoraId",
                        column: x => x.MoraId,
                        principalTable: "Moras",
                        principalColumn: "MoraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamo",
                columns: table => new
                {
                    PrestamoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Concepto = table.Column<string>(type: "TEXT", nullable: false),
                    Monto = table.Column<double>(type: "REAL", nullable: false),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Visibilidad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamo", x => x.PrestamoId);
                    table.ForeignKey(
                        name: "FK_Prestamo_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MorasDetalle_MoraId",
                table: "MorasDetalle",
                column: "MoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_PersonaId",
                table: "Prestamo",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MorasDetalle");

            migrationBuilder.DropTable(
                name: "Prestamo");

            migrationBuilder.DropTable(
                name: "Moras");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}

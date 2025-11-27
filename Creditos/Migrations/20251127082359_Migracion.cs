using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Creditos.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asesor",
                columns: table => new
                {
                    IdAsesor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsesorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asesor", x => x.IdAsesor);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCredito",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAsesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCredito", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudCredito_Asesor_IdAsesor",
                        column: x => x.IdAsesor,
                        principalTable: "Asesor",
                        principalColumn: "IdAsesor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionAsesor",
                columns: table => new
                {
                    IdNotificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAsesor = table.Column<int>(type: "int", nullable: false),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionAsesor", x => x.IdNotificacion);
                    table.ForeignKey(
                        name: "FK_NotificacionAsesor_Asesor_IdAsesor",
                        column: x => x.IdAsesor,
                        principalTable: "Asesor",
                        principalColumn: "IdAsesor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificacionAsesor_SolicitudCredito_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudCredito",
                        principalColumn: "IdSolicitud",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaCreditoFinanciera",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Plazo = table.Column<int>(type: "int", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condiciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaReproceso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaCreditoFinanciera", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK_RespuestaCreditoFinanciera_SolicitudCredito_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudCredito",
                        principalColumn: "IdSolicitud",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionAsesor_IdAsesor",
                table: "NotificacionAsesor",
                column: "IdAsesor");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionAsesor_IdSolicitud",
                table: "NotificacionAsesor",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaCreditoFinanciera_IdSolicitud",
                table: "RespuestaCreditoFinanciera",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCredito_IdAsesor",
                table: "SolicitudCredito",
                column: "IdAsesor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificacionAsesor");

            migrationBuilder.DropTable(
                name: "RespuestaCreditoFinanciera");

            migrationBuilder.DropTable(
                name: "SolicitudCredito");

            migrationBuilder.DropTable(
                name: "Asesor");
        }
    }
}

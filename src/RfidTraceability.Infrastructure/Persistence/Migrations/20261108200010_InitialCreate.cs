using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ordenes_compra",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordenes_compra", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "puntos_control",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puntos_control", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skus",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "programas",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_orden_compra = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programas", x => x.id);
                    table.ForeignKey(
                        name: "FK_programas_ordenes_compra_id_orden_compra",
                        column: x => x.id_orden_compra,
                        principalTable: "ordenes_compra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lotes",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_programa = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    cantidad_piezas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_lotes_programas_id_programa",
                        column: x => x.id_programa,
                        principalTable: "programas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "meta_programa",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    id_programa = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    id_punto_control = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    cantidad_meta = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meta_programa", x => x.id);
                    table.ForeignKey(
                        name: "FK_meta_programa_programas_id_programa",
                        column: x => x.id_programa,
                        principalTable: "programas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_meta_programa_puntos_control_id_punto_control",
                        column: x => x.id_punto_control,
                        principalTable: "puntos_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "piezas_rfid",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    epc = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    id_sku = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo_bloque = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_lote = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo_talla = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    codigo_lado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_piezas_rfid", x => x.id);
                    table.ForeignKey(
                        name: "FK_piezas_rfid_lotes_id_lote",
                        column: x => x.id_lote,
                        principalTable: "lotes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_piezas_rfid_skus_id_sku",
                        column: x => x.id_sku,
                        principalTable: "skus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lecturas_avance_pieza_punto_control",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    id_pieza_rfid = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    id_punto_control = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lecturas_avance_pieza_punto_control", x => x.id);
                    table.ForeignKey(
                        name: "FK_lecturas_avance_pieza_punto_control_piezas_rfid_id_pieza_rf~",
                        column: x => x.id_pieza_rfid,
                        principalTable: "piezas_rfid",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lecturas_avance_pieza_punto_control_puntos_control_id_punto~",
                        column: x => x.id_punto_control,
                        principalTable: "puntos_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_pieza_rfid_id_punto_~",
                table: "lecturas_avance_pieza_punto_control",
                columns: new[] { "id_pieza_rfid", "id_punto_control" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_punto_control",
                table: "lecturas_avance_pieza_punto_control",
                column: "id_punto_control");

            migrationBuilder.CreateIndex(
                name: "IX_lotes_id_programa_codigo",
                table: "lotes",
                columns: new[] { "id_programa", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_meta_programa_id_programa_id_punto_control_fecha",
                table: "meta_programa",
                columns: new[] { "id_programa", "id_punto_control", "fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_meta_programa_id_punto_control",
                table: "meta_programa",
                column: "id_punto_control");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_compra_codigo",
                table: "ordenes_compra",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_epc",
                table: "piezas_rfid",
                column: "epc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_id_lote",
                table: "piezas_rfid",
                column: "id_lote");

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_id_sku",
                table: "piezas_rfid",
                column: "id_sku");

            migrationBuilder.CreateIndex(
                name: "IX_programas_id_orden_compra_codigo",
                table: "programas",
                columns: new[] { "id_orden_compra", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_skus_codigo",
                table: "skus",
                column: "codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropTable(
                name: "meta_programa");

            migrationBuilder.DropTable(
                name: "piezas_rfid");

            migrationBuilder.DropTable(
                name: "puntos_control");

            migrationBuilder.DropTable(
                name: "lotes");

            migrationBuilder.DropTable(
                name: "skus");

            migrationBuilder.DropTable(
                name: "programas");

            migrationBuilder.DropTable(
                name: "ordenes_compra");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ordenes_compra",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    id_organizacion = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    origen = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'LOCAL'"),
                    id_externo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordenes_compra", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordenes_compra_organizaciones_id_organizacion",
                        column: x => x.id_organizacion,
                        principalTable: "organizaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "skus",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_organizacion = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skus", x => x.id);
                    table.ForeignKey(
                        name: "FK_skus_organizaciones_id_organizacion",
                        column: x => x.id_organizacion,
                        principalTable: "organizaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "unidades_negocio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_organizacion = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidades_negocio", x => x.id);
                    table.ForeignKey(
                        name: "FK_unidades_negocio_organizaciones_id_organizacion",
                        column: x => x.id_organizacion,
                        principalTable: "organizaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "programas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cantidad_objetivo = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio_planificada = table.Column<DateOnly>(type: "date", nullable: true),
                    fecha_fin_planificada = table.Column<DateOnly>(type: "date", nullable: true),
                    fecha_inicio_real = table.Column<DateOnly>(type: "date", nullable: true),
                    fecha_fin_real = table.Column<DateOnly>(type: "date", nullable: true),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'PLANIFICADO'"),
                    id_orden_compra = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
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
                name: "puntos_control",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_unidad_negocio = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puntos_control", x => x.id);
                    table.ForeignKey(
                        name: "FK_puntos_control_unidades_negocio_id_unidad_negocio",
                        column: x => x.id_unidad_negocio,
                        principalTable: "unidades_negocio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bloques",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_programa = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bloques", x => x.id);
                    table.ForeignKey(
                        name: "FK_bloques_programas_id_programa",
                        column: x => x.id_programa,
                        principalTable: "programas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lectores_rfid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_serie = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'ACTIVO'"),
                    id_punto_control = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lectores_rfid", x => x.id);
                    table.ForeignKey(
                        name: "FK_lectores_rfid_puntos_control_id_punto_control",
                        column: x => x.id_punto_control,
                        principalTable: "puntos_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "metas_programa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_programa = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_punto_control = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    cantidad_meta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metas_programa", x => x.id);
                    table.ForeignKey(
                        name: "FK_metas_programa_programas_id_programa",
                        column: x => x.id_programa,
                        principalTable: "programas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_metas_programa_puntos_control_id_punto_control",
                        column: x => x.id_punto_control,
                        principalTable: "puntos_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lotes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cantidad_pares = table.Column<int>(type: "integer", nullable: false),
                    id_bloque = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_lotes_bloques_id_bloque",
                        column: x => x.id_bloque,
                        principalTable: "bloques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "piezas_rfid",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    epc = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    id_sku = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_lote = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo_lado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
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
                name: "avances_pieza_punto_control",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_evento_origen = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    id_pieza_rfid = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_punto_control = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_lector_rfid = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avances_pieza_punto_control", x => x.id);
                    table.ForeignKey(
                        name: "FK_avances_pieza_punto_control_lectores_rfid_id_lector_rfid",
                        column: x => x.id_lector_rfid,
                        principalTable: "lectores_rfid",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_avances_pieza_punto_control_piezas_rfid_id_pieza_rfid",
                        column: x => x.id_pieza_rfid,
                        principalTable: "piezas_rfid",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_avances_pieza_punto_control_puntos_control_id_punto_control",
                        column: x => x.id_punto_control,
                        principalTable: "puntos_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_avances_pieza_punto_control_id_evento_origen",
                table: "avances_pieza_punto_control",
                column: "id_evento_origen",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_avances_pieza_punto_control_id_lector_rfid_fecha",
                table: "avances_pieza_punto_control",
                columns: new[] { "id_lector_rfid", "fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_avances_pieza_punto_control_id_pieza_rfid_fecha",
                table: "avances_pieza_punto_control",
                columns: new[] { "id_pieza_rfid", "fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_avances_pieza_punto_control_id_punto_control_fecha",
                table: "avances_pieza_punto_control",
                columns: new[] { "id_punto_control", "fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_bloques_id_programa_codigo",
                table: "bloques",
                columns: new[] { "id_programa", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lectores_rfid_id_punto_control_codigo",
                table: "lectores_rfid",
                columns: new[] { "id_punto_control", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lectores_rfid_numero_serie",
                table: "lectores_rfid",
                column: "numero_serie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lotes_id_bloque_codigo",
                table: "lotes",
                columns: new[] { "id_bloque", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metas_programa_id_programa_id_punto_control_fecha",
                table: "metas_programa",
                columns: new[] { "id_programa", "id_punto_control", "fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metas_programa_id_punto_control",
                table: "metas_programa",
                column: "id_punto_control");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_compra_id_organizacion_codigo",
                table: "ordenes_compra",
                columns: new[] { "id_organizacion", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_compra_id_organizacion_id_externo",
                table: "ordenes_compra",
                columns: new[] { "id_organizacion", "id_externo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organizaciones_codigo",
                table: "organizaciones",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_epc",
                table: "piezas_rfid",
                column: "epc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_id_lote_codigo_lado",
                table: "piezas_rfid",
                columns: new[] { "id_lote", "codigo_lado" },
                unique: true);

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
                name: "IX_puntos_control_id_unidad_negocio_codigo",
                table: "puntos_control",
                columns: new[] { "id_unidad_negocio", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_skus_id_organizacion_codigo",
                table: "skus",
                columns: new[] { "id_organizacion", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unidades_negocio_id_organizacion_codigo",
                table: "unidades_negocio",
                columns: new[] { "id_organizacion", "codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avances_pieza_punto_control");

            migrationBuilder.DropTable(
                name: "metas_programa");

            migrationBuilder.DropTable(
                name: "lectores_rfid");

            migrationBuilder.DropTable(
                name: "piezas_rfid");

            migrationBuilder.DropTable(
                name: "puntos_control");

            migrationBuilder.DropTable(
                name: "lotes");

            migrationBuilder.DropTable(
                name: "skus");

            migrationBuilder.DropTable(
                name: "unidades_negocio");

            migrationBuilder.DropTable(
                name: "bloques");

            migrationBuilder.DropTable(
                name: "programas");

            migrationBuilder.DropTable(
                name: "ordenes_compra");

            migrationBuilder.DropTable(
                name: "organizaciones");
        }
    }
}

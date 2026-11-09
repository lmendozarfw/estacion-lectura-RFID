using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDomainSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lecturas_avance_pieza_punto_control_piezas_rfid_id_pieza_rf~",
                table: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropForeignKey(
                name: "FK_lecturas_avance_pieza_punto_control_puntos_control_id_punto~",
                table: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropForeignKey(
                name: "FK_lotes_programas_id_programa",
                table: "lotes");

            migrationBuilder.DropForeignKey(
                name: "FK_meta_programa_programas_id_programa",
                table: "meta_programa");

            migrationBuilder.DropForeignKey(
                name: "FK_meta_programa_puntos_control_id_punto_control",
                table: "meta_programa");

            migrationBuilder.DropIndex(
                name: "IX_skus_codigo",
                table: "skus");

            migrationBuilder.DropIndex(
                name: "IX_piezas_rfid_id_lote",
                table: "piezas_rfid");

            migrationBuilder.DropIndex(
                name: "IX_ordenes_compra_codigo",
                table: "ordenes_compra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meta_programa",
                table: "meta_programa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lecturas_avance_pieza_punto_control",
                table: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_pieza_rfid_id_punto_~",
                table: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_punto_control",
                table: "lecturas_avance_pieza_punto_control");

            migrationBuilder.DropColumn(
                name: "fecha_fin",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "fecha_inicio",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "codigo_bloque",
                table: "piezas_rfid");

            migrationBuilder.DropColumn(
                name: "codigo_talla",
                table: "piezas_rfid");

            migrationBuilder.RenameTable(
                name: "meta_programa",
                newName: "metas_programa");

            migrationBuilder.RenameTable(
                name: "lecturas_avance_pieza_punto_control",
                newName: "avances_pieza_punto_control");

            migrationBuilder.RenameColumn(
                name: "id_programa",
                table: "lotes",
                newName: "id_bloque");

            migrationBuilder.RenameColumn(
                name: "cantidad_piezas",
                table: "lotes",
                newName: "cantidad_pares");

            migrationBuilder.RenameIndex(
                name: "IX_lotes_id_programa_codigo",
                table: "lotes",
                newName: "IX_lotes_id_bloque_codigo");

            migrationBuilder.RenameIndex(
                name: "IX_meta_programa_id_punto_control",
                table: "metas_programa",
                newName: "IX_metas_programa_id_punto_control");

            migrationBuilder.RenameIndex(
                name: "IX_meta_programa_id_programa_id_punto_control_fecha",
                table: "metas_programa",
                newName: "IX_metas_programa_id_programa_id_punto_control_fecha");

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "skus",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "skus",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_organizacion",
                table: "skus",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "puntos_control",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "codigo",
                table: "puntos_control",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "puntos_control",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_unidad_negocio",
                table: "puntos_control",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "cantidad_objetivo",
                table: "programas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "programas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValueSql: "'PLANIFICADO'");

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_fin_planificada",
                table: "programas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_fin_real",
                table: "programas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_inicio_planificada",
                table: "programas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_inicio_real",
                table: "programas",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "codigo_lado",
                table: "piezas_rfid",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha",
                table: "ordenes_compra",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_externo",
                table: "ordenes_compra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_organizacion",
                table: "ordenes_compra",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "origen",
                table: "ordenes_compra",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValueSql: "'LOCAL'");

            migrationBuilder.AddColumn<string>(
                name: "id_evento_origen",
                table: "avances_pieza_punto_control",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_lector_rfid",
                table: "avances_pieza_punto_control",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_metas_programa",
                table: "metas_programa",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_avances_pieza_punto_control",
                table: "avances_pieza_punto_control",
                column: "id");

            migrationBuilder.CreateTable(
                name: "bloques",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_programa = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
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
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_serie = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'ACTIVO'"),
                    id_punto_control = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
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
                name: "organizaciones",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidades_negocio",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_organizacion = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_skus_id_organizacion_codigo",
                table: "skus",
                columns: new[] { "id_organizacion", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_puntos_control_id_unidad_negocio_codigo",
                table: "puntos_control",
                columns: new[] { "id_unidad_negocio", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_id_lote_codigo_lado",
                table: "piezas_rfid",
                columns: new[] { "id_lote", "codigo_lado" },
                unique: true);

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
                name: "IX_organizaciones_codigo",
                table: "organizaciones",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unidades_negocio_id_organizacion_codigo",
                table: "unidades_negocio",
                columns: new[] { "id_organizacion", "codigo" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_avances_pieza_punto_control_lectores_rfid_id_lector_rfid",
                table: "avances_pieza_punto_control",
                column: "id_lector_rfid",
                principalTable: "lectores_rfid",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_avances_pieza_punto_control_piezas_rfid_id_pieza_rfid",
                table: "avances_pieza_punto_control",
                column: "id_pieza_rfid",
                principalTable: "piezas_rfid",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_avances_pieza_punto_control_puntos_control_id_punto_control",
                table: "avances_pieza_punto_control",
                column: "id_punto_control",
                principalTable: "puntos_control",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lotes_bloques_id_bloque",
                table: "lotes",
                column: "id_bloque",
                principalTable: "bloques",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_metas_programa_programas_id_programa",
                table: "metas_programa",
                column: "id_programa",
                principalTable: "programas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_metas_programa_puntos_control_id_punto_control",
                table: "metas_programa",
                column: "id_punto_control",
                principalTable: "puntos_control",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ordenes_compra_organizaciones_id_organizacion",
                table: "ordenes_compra",
                column: "id_organizacion",
                principalTable: "organizaciones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_puntos_control_unidades_negocio_id_unidad_negocio",
                table: "puntos_control",
                column: "id_unidad_negocio",
                principalTable: "unidades_negocio",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_skus_organizaciones_id_organizacion",
                table: "skus",
                column: "id_organizacion",
                principalTable: "organizaciones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_avances_pieza_punto_control_lectores_rfid_id_lector_rfid",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropForeignKey(
                name: "FK_avances_pieza_punto_control_piezas_rfid_id_pieza_rfid",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropForeignKey(
                name: "FK_avances_pieza_punto_control_puntos_control_id_punto_control",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropForeignKey(
                name: "FK_lotes_bloques_id_bloque",
                table: "lotes");

            migrationBuilder.DropForeignKey(
                name: "FK_metas_programa_programas_id_programa",
                table: "metas_programa");

            migrationBuilder.DropForeignKey(
                name: "FK_metas_programa_puntos_control_id_punto_control",
                table: "metas_programa");

            migrationBuilder.DropForeignKey(
                name: "FK_ordenes_compra_organizaciones_id_organizacion",
                table: "ordenes_compra");

            migrationBuilder.DropForeignKey(
                name: "FK_puntos_control_unidades_negocio_id_unidad_negocio",
                table: "puntos_control");

            migrationBuilder.DropForeignKey(
                name: "FK_skus_organizaciones_id_organizacion",
                table: "skus");

            migrationBuilder.DropTable(
                name: "bloques");

            migrationBuilder.DropTable(
                name: "lectores_rfid");

            migrationBuilder.DropTable(
                name: "unidades_negocio");

            migrationBuilder.DropTable(
                name: "organizaciones");

            migrationBuilder.DropIndex(
                name: "IX_skus_id_organizacion_codigo",
                table: "skus");

            migrationBuilder.DropIndex(
                name: "IX_puntos_control_id_unidad_negocio_codigo",
                table: "puntos_control");

            migrationBuilder.DropIndex(
                name: "IX_piezas_rfid_id_lote_codigo_lado",
                table: "piezas_rfid");

            migrationBuilder.DropIndex(
                name: "IX_ordenes_compra_id_organizacion_codigo",
                table: "ordenes_compra");

            migrationBuilder.DropIndex(
                name: "IX_ordenes_compra_id_organizacion_id_externo",
                table: "ordenes_compra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_metas_programa",
                table: "metas_programa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_avances_pieza_punto_control",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_avances_pieza_punto_control_id_evento_origen",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_avances_pieza_punto_control_id_lector_rfid_fecha",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_avances_pieza_punto_control_id_pieza_rfid_fecha",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropIndex(
                name: "IX_avances_pieza_punto_control_id_punto_control_fecha",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "skus");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "skus");

            migrationBuilder.DropColumn(
                name: "id_organizacion",
                table: "skus");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "puntos_control");

            migrationBuilder.DropColumn(
                name: "codigo",
                table: "puntos_control");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "puntos_control");

            migrationBuilder.DropColumn(
                name: "id_unidad_negocio",
                table: "puntos_control");

            migrationBuilder.DropColumn(
                name: "cantidad_objetivo",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "fecha_fin_planificada",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "fecha_fin_real",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "fecha_inicio_planificada",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "fecha_inicio_real",
                table: "programas");

            migrationBuilder.DropColumn(
                name: "id_externo",
                table: "ordenes_compra");

            migrationBuilder.DropColumn(
                name: "id_organizacion",
                table: "ordenes_compra");

            migrationBuilder.DropColumn(
                name: "origen",
                table: "ordenes_compra");

            migrationBuilder.DropColumn(
                name: "id_evento_origen",
                table: "avances_pieza_punto_control");

            migrationBuilder.DropColumn(
                name: "id_lector_rfid",
                table: "avances_pieza_punto_control");

            migrationBuilder.RenameTable(
                name: "metas_programa",
                newName: "meta_programa");

            migrationBuilder.RenameTable(
                name: "avances_pieza_punto_control",
                newName: "lecturas_avance_pieza_punto_control");

            migrationBuilder.RenameColumn(
                name: "id_bloque",
                table: "lotes",
                newName: "id_programa");

            migrationBuilder.RenameColumn(
                name: "cantidad_pares",
                table: "lotes",
                newName: "cantidad_piezas");

            migrationBuilder.RenameIndex(
                name: "IX_lotes_id_bloque_codigo",
                table: "lotes",
                newName: "IX_lotes_id_programa_codigo");

            migrationBuilder.RenameIndex(
                name: "IX_metas_programa_id_punto_control",
                table: "meta_programa",
                newName: "IX_meta_programa_id_punto_control");

            migrationBuilder.RenameIndex(
                name: "IX_metas_programa_id_programa_id_punto_control_fecha",
                table: "meta_programa",
                newName: "IX_meta_programa_id_programa_id_punto_control_fecha");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_fin",
                table: "programas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_inicio",
                table: "programas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "codigo_lado",
                table: "piezas_rfid",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<string>(
                name: "codigo_bloque",
                table: "piezas_rfid",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "codigo_talla",
                table: "piezas_rfid",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha",
                table: "ordenes_compra",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_meta_programa",
                table: "meta_programa",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lecturas_avance_pieza_punto_control",
                table: "lecturas_avance_pieza_punto_control",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_skus_codigo",
                table: "skus",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_piezas_rfid_id_lote",
                table: "piezas_rfid",
                column: "id_lote");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_compra_codigo",
                table: "ordenes_compra",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_pieza_rfid_id_punto_~",
                table: "lecturas_avance_pieza_punto_control",
                columns: new[] { "id_pieza_rfid", "id_punto_control" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lecturas_avance_pieza_punto_control_id_punto_control",
                table: "lecturas_avance_pieza_punto_control",
                column: "id_punto_control");

            migrationBuilder.AddForeignKey(
                name: "FK_lecturas_avance_pieza_punto_control_piezas_rfid_id_pieza_rf~",
                table: "lecturas_avance_pieza_punto_control",
                column: "id_pieza_rfid",
                principalTable: "piezas_rfid",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lecturas_avance_pieza_punto_control_puntos_control_id_punto~",
                table: "lecturas_avance_pieza_punto_control",
                column: "id_punto_control",
                principalTable: "puntos_control",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lotes_programas_id_programa",
                table: "lotes",
                column: "id_programa",
                principalTable: "programas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_meta_programa_programas_id_programa",
                table: "meta_programa",
                column: "id_programa",
                principalTable: "programas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_meta_programa_puntos_control_id_punto_control",
                table: "meta_programa",
                column: "id_punto_control",
                principalTable: "puntos_control",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

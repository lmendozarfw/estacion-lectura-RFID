using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthenticationAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "modulos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_organizacion = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    employee_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_organizaciones_id_organizacion",
                        column: x => x.id_organizacion,
                        principalTable: "organizaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_modulo = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    codigo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id);
                    table.ForeignKey(
                        name: "FK_permisos_modulos_id_modulo",
                        column: x => x.id_modulo,
                        principalTable: "modulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_roles",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_rol = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_roles", x => new { x.id_usuario, x.id_rol });
                    table.ForeignKey(
                        name: "FK_usuarios_roles_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles_permisos",
                columns: table => new
                {
                    id_rol = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_permiso = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_permisos", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_roles_permisos_permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_permisos_roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_modulos_codigo",
                table: "modulos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permisos_id_modulo_codigo",
                table: "permisos",
                columns: new[] { "id_modulo", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_codigo",
                table: "roles",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_permisos_id_permiso",
                table: "roles_permisos",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_organizacion_username",
                table: "usuarios",
                columns: new[] { "id_organizacion", "username" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_roles_id_rol",
                table: "usuarios_roles",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles_permisos");

            migrationBuilder.DropTable(
                name: "usuarios_roles");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "modulos");
        }
    }
}

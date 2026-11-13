using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "usuarios");

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    numero_empleado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombres = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    apellido_paterno = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    apellido_materno = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.id);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "id_empleado",
                table: "usuarios",
                type: "uuid",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_empleado",
                table: "usuarios",
                column: "id_empleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleados_numero_empleado",
                table: "empleados",
                column: "numero_empleado",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_empleados_id_empleado",
                table: "usuarios",
                column: "id_empleado",
                principalTable: "empleados",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_empleados_id_empleado",
                table: "usuarios");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_empleado",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "id_empleado",
                table: "usuarios");

            migrationBuilder.AddColumn<string>(
                name: "employee_id",
                table: "usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

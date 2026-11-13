using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_organizacion_username",
                table: "usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_organizacion",
                table: "usuarios",
                column: "id_organizacion");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_username",
                table: "usuarios",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_id_organizacion",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_username",
                table: "usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_organizacion_username",
                table: "usuarios",
                columns: new[] { "id_organizacion", "username" },
                unique: true);
        }
    }
}

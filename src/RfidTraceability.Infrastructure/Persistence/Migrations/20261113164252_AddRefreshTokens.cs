using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RfidTraceability.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tokens_refresco",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    token_hash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_expiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_revocacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reemplazado_por = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens_refresco", x => x.id);
                    table.ForeignKey(
                        name: "FK_tokens_refresco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tokens_refresco_id_usuario",
                table: "tokens_refresco",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_refresco_token_hash",
                table: "tokens_refresco",
                column: "token_hash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tokens_refresco");
        }
    }
}

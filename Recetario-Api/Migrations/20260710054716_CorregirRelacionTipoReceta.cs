using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recetario_Api.Migrations
{
    /// <inheritdoc />
    public partial class CorregirRelacionTipoReceta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tipo_Receta_RecetaId",
                table: "tipo");

            migrationBuilder.DropIndex(
                name: "IX_tipo_RecetaId",
                table: "tipo");

            migrationBuilder.DropColumn(
                name: "RecetaId",
                table: "tipo");

            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Receta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receta_TipoId",
                table: "Receta",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_tipo_TipoId",
                table: "Receta",
                column: "TipoId",
                principalTable: "tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receta_tipo_TipoId",
                table: "Receta");

            migrationBuilder.DropIndex(
                name: "IX_Receta_TipoId",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Receta");

            migrationBuilder.AddColumn<int>(
                name: "RecetaId",
                table: "tipo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tipo_RecetaId",
                table: "tipo",
                column: "RecetaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tipo_Receta_RecetaId",
                table: "tipo",
                column: "RecetaId",
                principalTable: "Receta",
                principalColumn: "RecetaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

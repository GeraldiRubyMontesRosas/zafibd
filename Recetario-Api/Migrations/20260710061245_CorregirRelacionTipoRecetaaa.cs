using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recetario_Api.Migrations
{
    /// <inheritdoc />
    public partial class CorregirRelacionTipoRecetaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "tipo",
                newName: "Nombres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "tipo",
                newName: "Nombre");
        }
    }
}

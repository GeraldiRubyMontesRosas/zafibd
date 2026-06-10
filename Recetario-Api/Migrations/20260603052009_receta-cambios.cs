using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recetario_Api.Migrations
{
    /// <inheritdoc />
    public partial class recetacambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Receta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Porciones",
                table: "Receta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoPreparacionMin",
                table: "Receta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "Porciones",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "TiempoPreparacionMin",
                table: "Receta");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recetario_Api.Migrations
{
    /// <inheritdoc />
    public partial class PreferenciaAlimenticia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receta_tipo_TipoId",
                table: "Receta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo",
                table: "tipo");

            migrationBuilder.RenameTable(
                name: "tipo",
                newName: "Tipo");

            migrationBuilder.AddColumn<int>(
                name: "PreferenciaAlimentariaId",
                table: "Receta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tipo",
                table: "Tipo",
                column: "TipoId");

            migrationBuilder.CreateTable(
                name: "PreferenciaAlimentaria",
                columns: table => new
                {
                    PreferenciaAlimentariaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciaAlimentaria", x => x.PreferenciaAlimentariaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_PreferenciaAlimentariaId",
                table: "Receta",
                column: "PreferenciaAlimentariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_PreferenciaAlimentaria_PreferenciaAlimentariaId",
                table: "Receta",
                column: "PreferenciaAlimentariaId",
                principalTable: "PreferenciaAlimentaria",
                principalColumn: "PreferenciaAlimentariaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_Tipo_TipoId",
                table: "Receta",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receta_PreferenciaAlimentaria_PreferenciaAlimentariaId",
                table: "Receta");

            migrationBuilder.DropForeignKey(
                name: "FK_Receta_Tipo_TipoId",
                table: "Receta");

            migrationBuilder.DropTable(
                name: "PreferenciaAlimentaria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tipo",
                table: "Tipo");

            migrationBuilder.DropIndex(
                name: "IX_Receta_PreferenciaAlimentariaId",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "PreferenciaAlimentariaId",
                table: "Receta");

            migrationBuilder.RenameTable(
                name: "Tipo",
                newName: "tipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo",
                table: "tipo",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_tipo_TipoId",
                table: "Receta",
                column: "TipoId",
                principalTable: "tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Recetario_Api.DTOs;

namespace Recetario_Api.Entities
{
    public class IngredienteReceta
    {
        public int IngredienteRecetaId { get; set; }
        public int Recetaid { get; set; }
        public Receta? Receta { get; set; }
        public int IngredienteId { get; set; }
        public Ingrediente? Ingrediente { get; set; }
        public decimal Cantidad { get; set; }
        public int UnidadId { get; set; }
        public Unidad? Unidad { get; set; }
    }
}

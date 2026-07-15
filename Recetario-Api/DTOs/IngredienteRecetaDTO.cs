namespace Recetario_Api.DTOs
{
    public class IngredienteRecetaDTO
    {
        public int IngredienteRecetaId { get; set; }
        public int Recetaid { get; set; }
        public int IngredienteId { get; set; }
        public decimal Cantidad { get; set; }
        public int UnidadId { get; set; }

    }
}

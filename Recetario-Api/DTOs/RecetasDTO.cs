namespace Recetario_Api.DTOs
{
    public class RecetasDTO
    {
        public int Recetaid { get; set; }
        public string Nombre { get; set; }
        public List<PasosDTO> PasosL { get; set; }
        public string FotoUrl { get; set; }
        public List<IngredienteRecetaDTO> Ingredientes { get; set; }
    }
}

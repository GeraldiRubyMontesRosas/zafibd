namespace Recetario_Api.DTOs
{
    public class RecetasDTO
    {
        public int Recetaid { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FotoUrl { get; set; }
        public int TiempoPreparacionMin { get; set; }
        public int Porciones { get; set; }
        public bool Horno { get; set; }
        public int TipoId { get; set; }
        public int PreferenciaAlimentariaId { get; set; }
        public List<PasosDTO> PasosL { get; set; }
        public List<IngredienteRecetaDTO> Ingredientes { get; set; }
        public List<UtensiliosDTO> Utensilios { get; set; }
    }
}

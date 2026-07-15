using Recetario_Api.DTOs;

namespace Recetario_Api.Entities
{
    public class Receta
    {
        public int RecetaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FotoUrl { get; set; }
        public int TiempoPreparacionMin { get; set; }
        public int Porciones { get; set; }
        public bool Horno { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public int PreferenciaAlimentariaId { get; set; }
        public PreferenciaAlimentaria PreferenciaAlimentaria { get; set; }
        public List<Paso> PasosL { get; set; }
        public List<Utensilio> Utensilios { get; set; }
        public List<IngredienteReceta> Ingredientes { get; set; }
    }
}

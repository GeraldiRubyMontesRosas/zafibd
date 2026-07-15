namespace Recetario_Api.Entities
{
    public class PreferenciaAlimentaria
    {
        public int PreferenciaAlimentariaId { get; set; }

        public string Nombre { get; set; }

        public List<Receta> Recetas { get; set; }
    }
}

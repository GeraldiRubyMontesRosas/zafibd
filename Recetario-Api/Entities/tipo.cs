namespace Recetario_Api.Entities
{
    public class Tipo
    {
        public int TipoId { get; set; }

        public string Nombre { get; set; }

        public List<Receta> Recetas { get; set; }
    }
}

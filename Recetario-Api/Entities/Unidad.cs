namespace Recetario_Api.Entities
{
    public class Unidad
    {
        public int UnidadId { get; set; }
        public string Unidades { get; set; }
        public List<IngredienteReceta> Ingredientes { get; set; }
    }
}

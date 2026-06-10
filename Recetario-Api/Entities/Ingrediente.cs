namespace Recetario_Api.Entities
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string Nombre { get; set; }
        public List<IngredienteReceta> Recetas { get; set; }
    }
}

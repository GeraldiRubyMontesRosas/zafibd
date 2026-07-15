namespace Recetario_Api.Entities
{
    public class Utensilio
    {
        public int UtensilioId { get; set; }
        public int RecetaId { get; set; }
        public Receta Receta { get; set; }
        public string Utensilios { get; set; }
        public int Cantidad { get; set; }
    }
}

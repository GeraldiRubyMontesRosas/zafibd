namespace Recetario_Api.Entities
{
    public class Paso
    {
        public int PasoId { get; set; }
        public int RecetaId { get; set; }
        public int NumeroPasos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Receta Receta { get; set; }
    }
}

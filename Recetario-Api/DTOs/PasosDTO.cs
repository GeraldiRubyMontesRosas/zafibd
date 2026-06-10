namespace Recetario_Api.DTOs
{
    public class PasosDTO
    {
        public int PasoId { get; set; }
        public int RecetaId { get; set; }
        public int NumeroPasos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}

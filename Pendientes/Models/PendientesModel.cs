namespace Pendientes.Models
{
    public class PendientesModel
    {
        public int ID { set; get; }
        public string? Titulo { set; get; }
        public string? Descripcion { set; get; }
        public DateTime Fecha_Creacion { set; get; }
        public DateTime Fecha_Vencimiento { set; get; }
        public string? Completada { set; get; } // I,P y T (Iniciada, Pausada y Terminada) 
    }
}

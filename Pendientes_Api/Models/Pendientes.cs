namespace Pendientes_Api.Models
{
    public class Pendientes
    {
        // ID, Titulo, Descripción, Fecha Creacion, Fecha Vencimiento, Completada
        public int ID { set; get;}
        public string ? Titulo { set; get;}
        public string ? Descripcion { set; get;}
        public DateTime Fecha_Creacion {set; get;}
        public DateTime Fecha_Vencimiento {set; get;}
        public string ? Completada { set; get; } // I,P y T (Iniciada, Pausada y Terminada) 
    }
}

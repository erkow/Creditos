using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Creditos.Models
{
    public class NotificacionAsesor
    {
        [Key]
        public int IdNotificacion { get; set; }
        public int IdAsesor { get; set; }
        
        public Asesor Asesor { get; set; }

        public int IdSolicitud { get; set; }
        
        public SolicitudCredito Solicitud { get; set; }

        public String Mensaje { get; set; } = string.Empty;
        public DateTime Fecha   { get; set; }
        public bool Leido   { get; set; }

    }
}

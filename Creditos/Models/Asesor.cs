using System.ComponentModel.DataAnnotations;

namespace Creditos.Models
{
    public class Asesor
    {
        [Key]
        public int IdAsesor { get; set; }

        public string AsesorNombre { get; set; } = string.Empty;

        public ICollection<NotificacionAsesor> Notificaciones { get; set; } = new List<NotificacionAsesor>();

        public ICollection<SolicitudCredito> Solicitudes { get; set; } = new List<SolicitudCredito>();
    }
}

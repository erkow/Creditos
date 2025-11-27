using System.ComponentModel.DataAnnotations;

namespace Creditos.Models
{
    public class SolicitudCredito
    {
        [Key]
        public int IdSolicitud {  get; set; }
        public string NumeroSolicitud { get; set; } = string.Empty;

        public int IdAsesor { get; set; }
        public Asesor Asesor { get; set; }

        public ICollection<RespuestaCreditoFinanciera> RespuestasCreditos { get; set; } = new List<RespuestaCreditoFinanciera>();

        
    }
}

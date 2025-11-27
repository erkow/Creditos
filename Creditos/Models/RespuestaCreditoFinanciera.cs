using Creditos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Creditos.Models
{
    public class RespuestaCreditoFinanciera
    {
        [Key]
        public int IdRespuesta {  get; set; }
        
        public int IdSolicitud { get; set; }
        public SolicitudCredito SolicitudCredito { get; set; }
               
        public EstadoCredito Estado { get; set; } = EstadoCredito.EN_PROCESO;

        public decimal Monto { get; set; }

        public int Plazo { get; set; } = 0;

        public DateTime FechaRespuesta { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string Condiciones { get; set; } = string.Empty;
        public string JsonCompleto { get; set; } = string.Empty;

        public DateTime FechaReproceso { get; set; }
    }
}

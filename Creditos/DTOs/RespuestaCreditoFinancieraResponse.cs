using Creditos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Creditos.DTOs
{
    public class RespuestaCreditoFinancieraResponse
    {
        public int IdRespuesta { get; set; }

        public int IdSolicitud { get; set; }

        public string NumeroSolicitud { get; set; } = string.Empty;

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

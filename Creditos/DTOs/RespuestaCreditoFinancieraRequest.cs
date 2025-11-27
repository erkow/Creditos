using Creditos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Creditos.DTOs
{
    public class RespuestaCreditoFinancieraRequest : IValidatableObject
    {
        [Required]
        public string NumeroSolicitud {  get; set; } = string.Empty;
        [Required]
        public EstadoCredito Estado { get; set; } = EstadoCredito.EN_PROCESO;

        public decimal MontoAprobado { get; set; }

        public float Tasa { get; set; } = 0;

        public int Plazo { get; set; } = 0;

        public DateTime FechaRespuesta { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public string Condiciones { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (Estado)
            {
                case EstadoCredito.APROBADO:
                    if (MontoAprobado <= 0) {
                        yield return new ValidationResult("Se requiere Monto valido", new[] { nameof(MontoAprobado) });
                    }
                    if (Plazo <= 0)
                    {
                        yield return new ValidationResult("Se requiere Plazo valido", new[] { nameof(Plazo) });
                    }
                    break;


                case EstadoCredito.CONDICIONADO:
                if (string.IsNullOrEmpty(Condiciones)) {
                        yield return new ValidationResult("Se requiere Condiciones", new[] { nameof(Condiciones) });
                    }
                    break;
                case EstadoCredito.NEGADO:
                    if (string.IsNullOrEmpty(Observacion)) {
                        yield return new ValidationResult("Se requiere Observacion", new[] { nameof(Observacion) });
                    }
                    break;
            }            
        }
    }
}

using Creditos.DTOs;

namespace Creditos.Models
{
    public static class RespuestaCreditoMapeo
    {
        public static RespuestaCreditoFinancieraResponse MapearRespuesta(this RespuestaCreditoFinanciera rcf)
        {
            return new RespuestaCreditoFinancieraResponse
            {
                IdRespuesta = rcf.IdRespuesta,
                IdSolicitud = rcf.IdSolicitud,
                NumeroSolicitud = rcf.SolicitudCredito?.NumeroSolicitud ?? string.Empty,
                Estado = rcf.Estado,
                Monto = rcf.Monto,
                Plazo = rcf.Plazo,
                FechaRespuesta = rcf.FechaRespuesta,
                Observaciones = rcf.Observaciones,
                Condiciones = rcf.Condiciones,
                JsonCompleto = rcf.JsonCompleto,
                FechaReproceso = rcf.FechaReproceso,
            };
        }
    }
}

using Creditos.DTOs;

namespace Creditos.Services
{
    public interface IRespuestaCreditoService
    {
        Task<RespuestaCreditoFinancieraResponse> CrearAsync(RespuestaCreditoFinancieraRequest request);
        Task<List<RespuestaCreditoFinancieraResponse>> ObtenerRespuestaPorNumeroSolicitudAsync(string numeroSolicitud);
    }
}

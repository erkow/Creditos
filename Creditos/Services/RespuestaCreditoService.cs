using Creditos.DTOs;
using Creditos.Enums;
using Creditos.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Creditos.Services
{
    public class RespuestaCreditoService : IRespuestaCreditoService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RespuestaCreditoService> _logger;
        private readonly IConfiguration _configuration;

        public RespuestaCreditoService(AppDbContext context, ILogger<RespuestaCreditoService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<RespuestaCreditoFinancieraResponse> CrearAsync(RespuestaCreditoFinancieraRequest request)
        {
            //Validacion existencia de solicitud
            var solicitud = await _context.SolicitudCredito
                .Include(a => a.Asesor)
                .FirstOrDefaultAsync(row => row.NumeroSolicitud == request.NumeroSolicitud);
            
            if (solicitud == null) {
                throw new ArgumentException($"Numero de solicitud {request.NumeroSolicitud} no existe");
            }

            DateTime? fechaReproceso = null;
            if (request.Estado == EstadoCredito.EN_PROCESO) {
                int segundosReproceso = _configuration.GetValue<int>("Financiera:SegundosReproceso", 20);
                fechaReproceso = DateTime.Now.AddSeconds(segundosReproceso);
            }
            
            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var rcf = new RespuestaCreditoFinanciera
                {
                    IdSolicitud = solicitud.IdSolicitud,
                    SolicitudCredito = solicitud,
                    Estado = request.Estado,
                    Monto = request.MontoAprobado,
                    Plazo = request.Plazo,
                    FechaRespuesta = request.FechaRespuesta,
                    Observaciones = request.Observacion,
                    Condiciones = request.Condiciones,
                    JsonCompleto = JsonSerializer.Serialize(request),
                    FechaReproceso = fechaReproceso ?? DateTime.MinValue
                };

                _context.RespuestaCreditoFinanciera.Add(rcf);                
                await _context.SaveChangesAsync();

                var notificacion = new NotificacionAsesor
                {
                    IdAsesor = solicitud.IdAsesor,
                    IdSolicitud = solicitud.IdSolicitud,                    
                    Mensaje = $"La financiera respondio {request.Estado} para la solicitud {request.NumeroSolicitud}",
                    Leido = false,
                    Fecha = DateTime.Now,
                };

                _context.NotificacionAsesor.Add(notificacion);
                await _context.SaveChangesAsync();

                await transaccion.CommitAsync();

                return rcf.MapearRespuesta();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar Respuesta Credito Financiera");
                throw new Exception("Error en base de datos", ex);
            }            
            
        }

        

        public async Task<List<RespuestaCreditoFinancieraResponse>> ObtenerRespuestaPorNumeroSolicitudAsync(string numeroSolicitud)
        {
            var rcfList = await _context.RespuestaCreditoFinanciera
                .Include(r => r.SolicitudCredito)
                .Where(row => row.SolicitudCredito.NumeroSolicitud == numeroSolicitud)                
                .ToListAsync();

            if (!rcfList.Any()) { 
                return new List<RespuestaCreditoFinancieraResponse>();
            }

            return rcfList.Select(rcf => rcf.MapearRespuesta()).ToList();           

        }
    }
}

using Creditos.DTOs;
using Creditos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Creditos.Controllers
{
    [ApiController]
    [Route("api/creditos")]
    public class RespuestaCreditoController : ControllerBase
    {
        private readonly IRespuestaCreditoService _service;
        
        public RespuestaCreditoController(IRespuestaCreditoService service)
        {
            _service = service;            
        }

        [HttpPost("respuesta")]
        public async Task<IActionResult> Crear([FromBody] RespuestaCreditoFinancieraRequest request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.CrearAsync(request);
                return CreatedAtAction(nameof(ObtenerPorNumeroSolicitud), new { numeroSolicitud = request.NumeroSolicitud }, result);
            }
            catch (KeyNotFoundException ex) { 
                return NotFound(new { mensaje = ex.Message});//404
            }
            catch (ArgumentException ex) {
                return BadRequest(new { mensaje = ex.Message });//400
            }
            catch (Exception ex)
            {                
                return StatusCode(500, new { message = "Error del servidor." });
            }
        }

        [HttpGet("respuesta/{numeroSolicitud}")]
        public async Task<IActionResult> ObtenerPorNumeroSolicitud(string numeroSolicitud) {
            
            var result = await _service.ObtenerRespuestaPorNumeroSolicitudAsync(numeroSolicitud);

            if (!result.Any()) { 
                return NotFound(new { mensaje = $"No se encontraron registros para solicitud {numeroSolicitud}" });                
            }

            return Ok(result);            
        }
    }
}

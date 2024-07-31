using CPAplicacion.Servicios;
using CPCore.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace miAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class miAPIController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public miAPIController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }


        [HttpGet("GetAllEventos")]
        public async Task<IActionResult> GetAllEventos()
        {
            var eventos = await _eventoService.GetEventos();
            return Ok(eventos);
        }



        [HttpPost("CrearEvento")]
        public async Task<IActionResult> CrearEvento([FromBody] Evento nuevoEvento)
        {
            if (nuevoEvento == null)
            {
                return BadRequest();
            }

            bool result = await _eventoService.AgregarEvento(nuevoEvento);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Error al crear el evento.");
            }
        }

        [HttpPut("UpdateEvento")]
        public async Task<IActionResult> UpdateEvento(Evento eventoActualizado)
        {
            bool result = await _eventoService.ActualizaEvento(eventoActualizado);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Error al actualizar el evento.");
            }
        }

        // DELETE: api/Eventos/{id}
        [HttpDelete("DeleteEvento")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            bool result = await _eventoService.EliminarEvento(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Error al eliminar el evento.");
            }
        }
    }
}

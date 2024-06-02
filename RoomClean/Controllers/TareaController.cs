using Domain.DTOS;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;

namespace RoomClean.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _adminServicio;

        public TareaController(ITareaService adminService)
        {
            _adminServicio = adminService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> ObtenerLista()
        {
            var response = await _adminServicio.ObtenerLista();
            return Ok(response);
        }
        [HttpGet("list/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var response = await _adminServicio.ObtenerPorId(id);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] TareaDto request)
        {
            var response = await _adminServicio.Crear(request);
            return Ok(response);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar([FromBody] TareaDto request, int id)
        {
            var response = await _adminServicio.Editar(request, id);
            return Ok(response);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var response = await _adminServicio.Eliminar(id);

            if (response.Succeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

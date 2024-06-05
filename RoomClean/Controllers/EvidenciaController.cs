using Domain.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;

namespace RoomClean.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("[controller]")]
    public class EvidenciaController : ControllerBase
    {
        private readonly IEvidenciaService _evidenciaService;

        public EvidenciaController(IEvidenciaService evidenciaService)
        {
            _evidenciaService = evidenciaService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> ObtenerLista()
        {
            var response = await _evidenciaService.ObtenerLista();
            return Ok(response);
        }
        [HttpGet("list/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var response = await _evidenciaService.ObtenerPorId(id);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] EvidenciaDto request)
        {
            var response = await _evidenciaService.Crear(request);
            return Ok(response);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar([FromBody] EvidenciaDto request, int id)
        {
            var response = await _evidenciaService.Editar(request, id);
            return Ok(response);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var response = await _evidenciaService.Eliminar(id);

            if (response.Succeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

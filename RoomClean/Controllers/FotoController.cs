using Domain.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;

namespace RoomClean.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FotoController : ControllerBase
    {
        private readonly IFotoService _fotoService;

        public FotoController(IFotoService fotoService)
        {
            _fotoService = fotoService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ObtenerLista()
        {
            var response = await _fotoService.ObtenerLista();
            return Ok(response);
        }
        [HttpGet("list/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var response = await _fotoService.ObtenerPorId(id);
            return Ok(response);
        }
        [Authorize(Roles = "Empleado")]
        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] FotoDto request)
        {
            var response = await _fotoService.Crear(request);
            return Ok(response);
        }
        [Authorize(Roles = "Empleado")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar([FromBody] FotoDto request, int id)
        {
            var response = await _fotoService.Editar(request, id);
            return Ok(response);
        }


        [Authorize(Roles = "Empleado")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var response = await _fotoService.Eliminar(id);

            if (response.Succeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

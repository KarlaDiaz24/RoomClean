using Domain.DTOS;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using RoomClean.Services;
using System.Security.Claims;

namespace RoomClean.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FotoController : ControllerBase
    {
        private readonly IFotoService _fotoService;
        private readonly ApplicationDBContext _context;
        public FotoController(IFotoService fotoService, ApplicationDBContext context)
        {
            _fotoService = fotoService;
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ObtenerLista()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            var response = await _fotoService.ObtenerLista();
            return Ok(response);
        }
        [HttpGet("list/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            var response = await _fotoService.ObtenerPorId(id);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] FotoDto request)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 2)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var response = await _fotoService.Crear(request);
            return Ok(response);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar([FromBody] FotoDto request, int id)
        {
            var response = await _fotoService.Editar(request, id);
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 2)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var response = await _fotoService.Eliminar(id);

            if (response.Succeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

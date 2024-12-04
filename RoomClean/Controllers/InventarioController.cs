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
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;
        private readonly ApplicationDBContext _context;
        public InventarioController(IInventarioService inventarioService, ApplicationDBContext context)
        {
            _inventarioService = inventarioService;
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

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            try
            {
                var response = await _inventarioService.ObtenerLista();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var response = await _inventarioService.ObtenerPorId(id);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] InventarioDto request)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            try
            {
                var response = await _inventarioService.Crear(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] InventarioDto request)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }


            try
            {
                var response = await _inventarioService.Editar(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            try
            {
                var response = await _inventarioService.Eliminar(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

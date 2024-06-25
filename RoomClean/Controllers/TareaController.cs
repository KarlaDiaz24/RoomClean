using Domain.DTOS;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Context;
using RoomClean.Services;
using System.Security.Claims;

namespace RoomClean.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _adminServicio;
        private readonly ApplicationDBContext _context;
        public TareaController(ITareaService adminService,ApplicationDBContext  context)
        {
            _adminServicio = adminService;
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

            var response = await _adminServicio.ObtenerLista(usuario.Id);
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

            var response = await _adminServicio.ObtenerPorId(id);
            return Ok(response);
        }



        [HttpPost("create")]
        public async Task<ActionResult> Crear([FromBody] TareaDto request)
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


            var response = await _adminServicio.Crear(request);
            return Ok(response);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Editar([FromBody] TareaDto request, int id)
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

            var response = await _adminServicio.Editar(request, id);
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

            if (usuario.FKRol != 1)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var response = await _adminServicio.Eliminar(id);

            if (response.Succeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

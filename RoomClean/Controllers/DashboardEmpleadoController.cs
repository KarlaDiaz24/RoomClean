using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using RoomClean.Services;
using System.Security.Claims;

namespace RoomClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardEmpleadoController : ControllerBase
    {
        private readonly ITareaService _tareas;
        private readonly ApplicationDBContext _context;

        public DashboardEmpleadoController(ITareaService tareasService, ApplicationDBContext context)
        {
            _tareas = tareasService;
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

            if (usuario.FKRol != 2)
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var response = await _tareas.ObtenerLista();
            return Ok(response);
        }
    }
}

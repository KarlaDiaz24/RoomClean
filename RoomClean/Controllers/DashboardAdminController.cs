using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;
using Domain.Entities   ;
using System.Security.Claims;
using RoomClean.Context;
using Microsoft.Extensions.Configuration;


namespace RoomClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ApplicationDBContext _context;


        public AdminController(IUsuarioService usuarioService, ApplicationDBContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Mostrarusuarios()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity,_context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol!= 1) 
            {
                return BadRequest("No tienes permisos para esta accion");

            }

            var users = await _usuarioService.ObtenerUsuarios();
            return Ok(users);
        }
    }


}

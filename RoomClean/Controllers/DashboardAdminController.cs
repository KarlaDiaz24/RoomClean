using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;
using Domain.Entities;

namespace RoomClean.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AdminController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Mostrarusuarios()
        {
            var users = await _usuarioService.ObtenerUsuarios();
            return Ok(users);
        }
    }


}

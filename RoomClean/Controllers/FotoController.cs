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

        [HttpGet("list/id")]
        public async Task<IActionResult> ObtenerLista(int Id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;



            var response = await _fotoService.ObtenerLista(Id);

            return Ok(response);
        }
        [HttpGet("{id}")]
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
        
        public async Task<ActionResult> Crear([FromForm] FotoDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = Jwt.Validartoken(identity, _context);

            if (!rtoken.success)
                return BadRequest(new { success = false, message = rtoken.message });

            Usuario usuario = rtoken.result;

            if (usuario.FKRol != 2)
            {
                return BadRequest("No tienes permisos para esta acción");
            }

            // Aquí manejas la lógica para guardar la URL de la foto en lugar del objeto IFormFile
            var foto = new Foto
            {
                FotoUrl = request.FotoUrl,  // Asignar la URL de la foto recibida
                FkEvidencia = request.FkEvidencia
            };

            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Foto guardada correctamente", fotoUrl = foto.FotoUrl });
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

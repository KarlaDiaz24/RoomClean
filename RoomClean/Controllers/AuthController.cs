using Domain.DTOS;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomClean.Context;
using RoomClean.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _context;

        public AuthController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioDto = new UsuarioDto
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Número = model.Número,
                Correo = model.Correo,
                Contraseña = model.Contraseña,
                FKRol = model.FKRol
            };

            var result = await _usuarioService.CrearUsuario(usuarioDto);
            if (!result.Succeded)
            {
                return BadRequest(new { errors = result.Message });
            }

            return Ok(new { result = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usuarioService.ValidarUsuario(model.Email, model.Password);
            if (!result.Succeded)
            {
                return Unauthorized(new { message = result.Message });
            }

            var token = GenerateJwtToken(model.Email);
            return Ok(new { token });
        }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.Id.ToString()),
                new Claim("rol", usuario.FKRol.ToString()),
                new Claim("usuario", usuario.Correo)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El número es obligatorio.")]
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string? Número { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string? Contraseña { get; set; }
        public string? Foto { get; set; }

        [Required(ErrorMessage = "Se requiere un rol")]
        public int FKRol { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "No se ha ingresado ningun correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contraseña { get; set; }
    }
}

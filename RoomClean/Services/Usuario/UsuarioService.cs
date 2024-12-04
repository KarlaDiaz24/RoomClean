using Domain.DTOS;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomClean.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDBContext _context;
        public UsuarioService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Usuario>>> ObtenerUsuarios()
        {
            try
            {
                List<Usuario> usuarios = await _context.Usuarios.Include(x => x.Roles).ToListAsync();
                return new Response<List<Usuario>>(usuarios);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ObtenerUsuario(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> CrearUsuario(UsuarioDto request)
        {
            try
            {
                string hashedPassword = ApplicationDBContext.ComputeSha256Hash(request.Contraseña);

                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Número = request.Número,
                    Correo = request.Correo,
                    Contraseña = hashedPassword,
                    Foto = request.Foto,
                    FKRol = request.FKRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ActualizarUsuario(int id, UsuarioDto usuarioDto)
        {
            try
            {
                string hashedPassword = ApplicationDBContext.ComputeSha256Hash(usuarioDto.Contraseña);
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario != null)
                {
                    usuario.Nombre = usuarioDto.Nombre;
                    usuario.Apellido = usuarioDto.Apellido;
                    usuario.Número = usuarioDto.Número;
                    usuario.Correo = usuarioDto.Correo;
                    usuario.Contraseña = hashedPassword;
                    usuario.Foto = usuarioDto.Foto;
                    usuario.FKRol = usuarioDto.FKRol;

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();

                    return new Response<Usuario>(usuario);
                }

                return new Response<Usuario>("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error al actualizar: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> EliminarUsuario(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                    return new Response<Usuario>("Usuario eliminado: " + usuario.Nombre);
                }

                return new Response<Usuario>("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar: " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ValidarUsuario(string correo, string contraseña)
        {
            try
            {
                string hashedPassword = ApplicationDBContext.ComputeSha256Hash(contraseña);
                Usuario usuario = await _context.Usuarios.Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.Correo == correo && x.Contraseña == hashedPassword);

                if (usuario != null)
                {
                    return new Response<Usuario>(usuario);
                }

                return new Response<Usuario>("Usuario o contraseña incorrectos");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario: " + ex.Message);
            }
        }

    }
}

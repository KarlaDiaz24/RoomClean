

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;

namespace RoomClean.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly ApplicationDBContext _context;
        public UsuarioService(ApplicationDBContext context)
        {
            _context = context;
        }
        //Lista de usuarios
        public async Task<Response<List<Usuario>>> ObtenerUsuarios()
        {
            try
            {
                List<Usuario> Response = await _context.usuario.Include(x => x.Roles).ToListAsync();

                return new Response<List<Usuario>>(Response);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ObtenerUsuario(int id)
        {
            try
            {
                Usuario  res = await _context.usuario.FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Usuario>(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public async Task<Response<Usuario>> CrearUsuario(UsuarioResponse request)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Número = request.Número,
                    Correo = request.Correo,
                    Contraseña= request.Contraseña,
                    Foto= request.Foto,
                    FKRol = request.FKRol
                };

                _context.usuario.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario);

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ActualizarUsuario(int id, UsuarioResponse usuario)
        {
            try
            {
                Usuario us = await _context.usuario.FirstOrDefaultAsync(x => x.Id == id);

                if (us != null)
                {
                    us.Nombre = usuario.Nombre;
                    us.Apellido = usuario.Apellido;
                    us.Número = usuario.Número;
                    us.Correo = usuario.Correo;
                    us.Contraseña = usuario.Contraseña;
                    us.Foto = usuario.Foto;
                    us.FKRol = usuario.FKRol;
                    _context.SaveChanges();
                }

                Usuario newUsuario = new Usuario()
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Número = usuario.Número,
                    Correo = usuario.Correo,
                    Contraseña = usuario.Contraseña,
                    Foto = usuario.Foto,
                    FKRol = usuario.FKRol
                };

                _context.usuario.Update(us);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(newUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al actualizar" + ex.Message);
            }
        }

        public async Task<Response<Usuario>> EliminarUsuario(int id)
        {
            try
            {
                Usuario us = await _context.usuario.FirstOrDefaultAsync(x => x.Id == id);
                if (us != null)
                {
                    _context.usuario.Remove(us);
                    await _context.SaveChangesAsync();
                    return new Response<Usuario>("Usuario eliminado:" + us.Nombre.ToString());
                }

                return new Response<Usuario>("Usuario no encontrado" + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }
    }
}

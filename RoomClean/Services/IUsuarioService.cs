using Domain.DTOS;
using Domain.Entities;

namespace RoomClean.Services
{
    public interface IUsuarioService
    {
        public Task<Response<List<Usuario>>> ObtenerUsuarios();

        public Task<Response<Usuario>> ObtenerUsuario(int id);

        public Task<Response<Usuario>> CrearUsuario(UsuarioDto request);

        public Task<Response<Usuario>> ActualizarUsuario(int id, UsuarioDto usuario);

        public Task<Response<Usuario>> EliminarUsuario(int id);
    }
}

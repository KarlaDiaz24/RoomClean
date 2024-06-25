using Domain.DTOS;
using Domain.Entities;

namespace RoomClean.Services
{
    public interface IFotoService
    {
        Task<Response<List<Foto>>> ObtenerLista(int Id);
        Task<Response<Foto>> ObtenerPorId(int id);
        Task<Response<Foto>> Crear(FotoDto request);
        Task<Response<Foto>> Editar(FotoDto request, int id);
        Task<Response<Foto>> Eliminar(int id);
    }
}

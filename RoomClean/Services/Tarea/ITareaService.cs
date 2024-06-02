using Domain.DTOS;
using Domain.Entities;

namespace RoomClean.Services
{
    public interface ITareaService
    {
        Task<Response<List<Tarea>>> ObtenerLista();
        Task<Response<Tarea>> ObtenerPorId(int id);
        Task<Response<Tarea>> Crear(TareaDto request);
        Task<Response<Tarea>> Editar(TareaDto request, int id);
        Task<Response<Tarea>> Eliminar(int id);
    }
}

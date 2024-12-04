using Domain.DTOS;
using Domain.Entities;

namespace RoomClean.Services
{
    public interface IInventarioService
    {
        Task<Response<List<Inventario>>> ObtenerLista();
        Task<Response<Inventario>> ObtenerPorId(int id);
        Task<Response<Inventario>> Crear(InventarioDto request);
        Task<Response<Inventario>> Editar(int id, InventarioDto request);
        Task<Response<Inventario>> Eliminar(int id);
    }
}

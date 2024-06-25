using Domain.DTOS;
using Domain.Entities;

namespace RoomClean.Services
{
    public interface IEvidenciaService
    {
        Task<Response<List<Evidencia>>> ObtenerLista(int Id);
        Task<Response<Evidencia>> ObtenerPorId(int id);
        Task<Response<Evidencia>> Crear(EvidenciaDto request);
        Task<Response<Evidencia>> Editar(EvidenciaDto request, int id);
        Task<Response<Evidencia>> Eliminar(int id);
    }
}

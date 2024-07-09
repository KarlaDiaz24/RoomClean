using Dapper;
using Domain.DTOS;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using System.Data;

namespace RoomClean.Services
{
    public class EvidenciaService : IEvidenciaService
    {
        private readonly ApplicationDBContext _context;
        public EvidenciaService(ApplicationDBContext context)
        {
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Evidencia>>> ObtenerLista(int Id)
        {
            try
            {
                List<Evidencia> response = new List<Evidencia>();
                var result = await _context.Database.GetDbConnection().QueryAsync<Evidencia>(
                    "PAEvidencia", 
                    new { Id }, commandType: CommandType.StoredProcedure);
                response = result.ToList();
                return new Response<List<Evidencia>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Evidencia>> ObtenerPorId(int id)
        {
            try
            {
                Evidencia response = await _context.Evidencias.FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Evidencia>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Evidencia>> Crear(EvidenciaDto request)
        {
            try
            {
                Evidencia evidencia = new Evidencia()
                {
                    Comentarios = request.Comentarios,
                    FKTarea = request.FKTarea,
                };
                _context.Evidencias.Add(evidencia);
                await _context.SaveChangesAsync();

                return new Response<Evidencia>(evidencia);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Evidencia>> Editar(EvidenciaDto request, int id)
        {
            try
            {
                Evidencia evidencia = await _context.Evidencias.FirstOrDefaultAsync(x => x.Id == id);

                if (evidencia == null)
                {
                    throw new Exception("No existe el usuario");
                }

                evidencia.Comentarios = request.Comentarios;
                evidencia.FKTarea = request.FKTarea;

                _context.Evidencias.Update(evidencia);
                await _context.SaveChangesAsync();
                return new Response<Evidencia>(evidencia);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Evidencia>> Eliminar(int id)
        {
            try
            {
                Evidencia evidencia = await _context.Evidencias.FirstOrDefaultAsync(x => x.Id == id);

                if (evidencia == null)
                {
                    throw new Exception("No existe el usuario");
                }

                _context.Evidencias.Remove(evidencia);
                await _context.SaveChangesAsync();
                return new Response<Evidencia>(evidencia);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
    }
}

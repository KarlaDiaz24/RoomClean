using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using System.Data;
using Dapper;
using Domain.DTOS;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomClean.Services
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDBContext _context;
        public TareaService(ApplicationDBContext context)
        {
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Tarea>>> ObtenerLista(int Id)
        {
            try
            {
                List<Tarea> response = new List<Tarea>();
                var result = await _context.Database.GetDbConnection().QueryAsync<Tarea>(
                    "PATarea",
                    new { UserId = Id },
                    commandType: CommandType.StoredProcedure
                );
                response = result.ToList();
                return new Response<List<Tarea>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }

        public async Task<Response<Tarea>> ObtenerPorId(int id)
        {
            try
            {
                Tarea response = await _context.Tareas.FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Tarea>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Tarea>> Crear(TareaDto request)
        {
            try
            {
                Tarea tarea = new Tarea()
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Estatus = request.Estatus,
                    FkUsuario = request.FkUsuario,
                };
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                return new Response<Tarea>(tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Tarea>> Editar(TareaDto request, int id)
        {
            try
            {
                Tarea tarea = await _context.Tareas.FirstOrDefaultAsync(x => x.Id == id);

                if (tarea == null)
                {
                    throw new Exception("No existe el usuario");
                }

                tarea.Nombre = request.Nombre;
                tarea.Descripcion = request.Descripcion;
                tarea.Estatus = request.Estatus;
                tarea.FkUsuario = request.FkUsuario;

                _context.Tareas.Update(tarea);
                await _context.SaveChangesAsync();
                return new Response<Tarea>(tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Tarea>> Eliminar(int id)
        {
            try
            {
                Tarea tarea = await _context.Tareas.FirstOrDefaultAsync(x => x.Id == id);

                if (tarea == null)
                {
                    throw new Exception("No existe el usuario");
                }

                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
                return new Response<Tarea>(tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
    }
}

using Dapper;
using Domain.DTOS;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RoomClean.Context;
using System.Data;

namespace RoomClean.Services
{
    public class FotoService : IFotoService
    {
        private readonly ApplicationDBContext _context;
        public FotoService(ApplicationDBContext context)
        {
            _context = context;
        }

        //Lista de fotos
        public async Task<Response<List<Foto>>> ObtenerLista()
        {
            try
            {
                List<Foto> response = new List<Foto>();
                var result = await _context.Database.GetDbConnection().QueryAsync<Foto>("PAFoto", new { }, commandType: CommandType.StoredProcedure);
                response = result.ToList();
                return new Response<List<Foto>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Foto>> ObtenerPorId(int id)
        {
            try
            {
                Foto response = await _context.Fotos.FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Foto>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Foto>> Crear(FotoDto request)
        {
            try
            {
                Foto foto = new Foto()
                {
                    FotoUrl = request.FotoUrl,
                    FkEvidencia = request.FkEvidencia,
                };
                _context.Fotos.Add(foto);
                await _context.SaveChangesAsync();

                return new Response<Foto>(foto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Foto>> Editar(FotoDto request, int id)
        {
            try
            {
                Foto foto = await _context.Fotos.FirstOrDefaultAsync(x => x.Id == id);

                if (foto == null)
                {
                    throw new Exception("No existe el usuario");
                }

                foto.FotoUrl = request.FotoUrl;
                foto.FkEvidencia = request.FkEvidencia;

                _context.Fotos.Update(foto);
                await _context.SaveChangesAsync();
                return new Response<Foto>(foto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
        public async Task<Response<Foto>> Eliminar(int id)
        {
            try
            {
                Foto foto = await _context.Fotos.FirstOrDefaultAsync(x => x.Id == id);

                if (foto == null)
                {
                    throw new Exception("No existe el usuario");
                }

                _context.Fotos.Remove(foto);
                await _context.SaveChangesAsync();
                return new Response<Foto>(foto);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
    }
}

using Domain.DTOS;
using RoomClean.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomClean.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly ApplicationDBContext _context;

        public InventarioService(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<Response<List<Inventario>>> ObtenerLista()
        {
            try
            {
                List<Inventario> Inventarios = await _context.Articulos.ToListAsync();
                return new Response<List<Inventario>>(Inventarios);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }
        public async Task<Response<Inventario>> ObtenerPorId(int id)
        {
            try
            {
                Inventario Inventario = await _context.Articulos.FirstOrDefaultAsync(x => x.Id == id);
                return new Response<Inventario>(Inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Inventario>> Crear(InventarioDto request)
        {

            try { 
            var inventario = new Inventario
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Cantidad = request.Cantidad
            };

            _context.Articulos.Add(inventario);
            await _context.SaveChangesAsync();

            return new Response<Inventario>(inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Inventario>> Editar(int id, InventarioDto request)
        {
            try { 
            var inventario = await _context.Articulos.FindAsync(id);

            if (inventario == null)
            {
                return new Response<Inventario>("Articulo no encontrado");
             }

            inventario.Nombre = request.Nombre;
            inventario.Descripcion = request.Descripcion;
            inventario.Cantidad = request.Cantidad;

            _context.Articulos.Update(inventario);
            await _context.SaveChangesAsync();

                return new Response<Inventario>(inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<Inventario>> Eliminar(int id)
        {
            try
            {
                Inventario inventario = await _context.Articulos.FirstOrDefaultAsync(x => x.Id == id);

                if (inventario == null)
                {
                    throw new Exception("No existe el articulo");
                }

                _context.Articulos.Remove(inventario);
                await _context.SaveChangesAsync();
                return new Response<Inventario>(inventario);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }
        }
    }
}

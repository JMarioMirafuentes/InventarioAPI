using AutoMapper;

using InventarioAPI.Dtos;
using InventarioAPI.Helpers.Exceptions;
using InventarioAPI.Helpers.Interfaces;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace InventarioAPI.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Alimentos y Bebidas.
    /// </summary>
    public class AlimentoBebidaServicio : IAlimentoBebida
    {
        private readonly InventarioDbContext _inventarioDbContext;

        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor del servicio de Alimentos y Bebidas.
        /// </summary>
        /// <param name="iventarioDbContext"></param>
        /// <param name="mapper"></param>
        public AlimentoBebidaServicio(InventarioDbContext inventarioDbContext, IMapper mapper)
        {
            _inventarioDbContext = inventarioDbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Actualiza un alimento o bebida por su ID.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Devuelve verdadero o falso segun su resultado</returns>
        public async Task<bool> ActualizarAlimentoBebida(AlimentoBebida dto)
        {
            ValidarId(dto.Id);
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ValidationException("El nombre es obligatorio.");
            var entity = await _inventarioDbContext.AlimentoBebidas.FindAsync(dto.Id);
            if (entity == null) throw new DataNotFoundException("No se encontró el alimento o bebida con el ID especificado.");

            entity.Nombre = dto.Nombre;
            entity.Descripcion = dto.Descripcion;
            entity.Estatus = dto.Estatus;

            await _inventarioDbContext.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Elimina un alimento o bebida por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> EliminarAlimentoBebidaId(int id)
        {
            ValidarId(id);

            var entity = await _inventarioDbContext.AlimentoBebidas.FindAsync(id);
            if (entity == null) throw new DataNotFoundException("No se encontró el alimento o bebida con el ID especificado.");

            _inventarioDbContext.AlimentoBebidas.Remove(entity);
            await _inventarioDbContext.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Guarda un nuevo alimento o bebida con un ID específico.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Devuelve verdadero o falso segun su resultado</returns>
        public async Task<AlimentoBebidaDTO> GuardarAlimentoBebida(AlimentoBebida dto)
        {

            ValidarId(dto.Id);

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ValidationException("El campo 'Nombre' es obligatorio y no puede estar vacío.");

            bool exists = await _inventarioDbContext.AlimentoBebidas
                          .AnyAsync(x => x.Nombre == dto.Nombre || x.Id == dto.Id);
            if (exists)
                throw new AlreadyExistsException("Ya existe un alimento o bebida registrado con el mismo nombre o ID.");

            _inventarioDbContext.AlimentoBebidas.Add(dto);
            await _inventarioDbContext.SaveChangesAsync();
            return _mapper.Map<AlimentoBebida,AlimentoBebidaDTO>(dto);
        }
        /// <summary>
        /// Obtiene un alimento o bebida por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AlimentoBebidaDTO?> ObtenerAlimentoBebidaId(int id)
        {
            ValidarId(id);

            var entity = await _inventarioDbContext.AlimentoBebidas.FindAsync(id);
            if (entity == null) throw new DataNotFoundException("No se encontró el alimento o bebida con el ID especificado.");

            return _mapper.Map<AlimentoBebida,AlimentoBebidaDTO>(entity);
        }
        /// <summary>
        /// Obtiene todos los alimentos y bebidas.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AlimentoBebidaDTO>> ObtenerTodos()
        {

            var result= await _inventarioDbContext.AlimentoBebidas.ToListAsync();

            return _mapper.Map<IEnumerable<AlimentoBebidaDTO>>(result);
        }
        /// <summary>
        /// Cambia el estatus de un alimento o bebida por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nuevoEstatus"></param>
        /// <returns></returns>
        public async Task<bool> CambiarEstatusAlimentoBebidaId(int id, bool nuevoEstatus)
        {
            ValidarId(id);

            var entity = await _inventarioDbContext.AlimentoBebidas.FindAsync(id);
            if (entity == null) throw new DataNotFoundException("No se encontró el alimento o bebida con el ID especificado.");

            entity.Estatus = nuevoEstatus ? true: false;

            await _inventarioDbContext.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Método para validar el ID de un alimento o bebida.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ValidationException"></exception>
        private void ValidarId(int id)
        {
            if (id <= 0)
                throw new ValidationException("El ID proporcionado es obligatorio y debe ser mayor que cero.");
        }


    }
}

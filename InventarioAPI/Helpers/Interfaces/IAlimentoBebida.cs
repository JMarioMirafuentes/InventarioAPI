using InventarioAPI.Dtos;
using InventarioAPI.Models;

namespace InventarioAPI.Helpers.Interfaces
{
    /// <summary>
    /// Interface para manejar operaciones CRUD de Alimentos y Bebidas.
    /// </summary>
    public interface IAlimentoBebida
    {
        Task<IEnumerable<AlimentoBebidaDTO>> ObtenerTodos();
        Task<AlimentoBebidaDTO?> ObtenerAlimentoBebidaId(int id);
        Task<AlimentoBebidaDTO> GuardarAlimentoBebida(AlimentoBebida dto);
        Task<bool> ActualizarAlimentoBebida(AlimentoBebida dto);
        Task<bool> EliminarAlimentoBebidaId(int id);
        Task<bool> CambiarEstatusAlimentoBebidaId(int id, bool nuevoEstatus);
    }
}
